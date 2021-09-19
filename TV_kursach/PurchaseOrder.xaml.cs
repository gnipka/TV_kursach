using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TV_kursach
{
    /// <summary>
    /// Логика взаимодействия для PurchaseOrder.xaml
    /// </summary>
    public partial class PurchaseOrder : Window
    {
        public PurchaseOrder()
        {
            InitializeComponent();
            try
            {
                ConnectionWithDB.OpenConnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = ConnectionWithDB.GetConnection();
                ; cmd.CommandText = "SELECT Storage_ID FROM storage";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboboxIDStore.Items.Add(reader[0].ToString()); ;
                }
                reader.Close();
                MySqlCommand cmdSec = new MySqlCommand();
                cmdSec.Connection = ConnectionWithDB.GetConnection();
                cmdSec.CommandText = "SELECT Nom_ID FROM nomenclature";
                MySqlDataReader readerSec = cmdSec.ExecuteReader();
                while (readerSec.Read())
                {
                    comboboxIDnom.Items.Add(readerSec[0].ToString()); ;
                }
                ConnectionWithDB.CloseConnection();
            }
            catch
            {
                MessageBox.Show("Нет соединения с базой данных:(");
            }

        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            int num;
            bool isnum = int.TryParse(textboxCount.Text, out num);
            if (isnum)
            {
                try
                {

                    int IDstore = Convert.ToInt32(comboboxIDStore.SelectedValue.ToString());
                    int IDnom = Convert.ToInt32(comboboxIDnom.SelectedValue.ToString());
                    int count = Convert.ToInt32(textboxCount.Text);

                    /*try
                    {*/
                    ConnectionWithDB.OpenConnection();
                    MySqlCommand cmdSearch = new MySqlCommand();
                    cmdSearch.Connection = ConnectionWithDB.GetConnection();
                    cmdSearch.CommandText = "SELECT St_Count FROM stocks WHERE Nom_ID = @IDnom AND Storage_ID = @IDstore";
                    cmdSearch.Parameters.AddWithValue("@IDnom", IDnom);
                    cmdSearch.Parameters.AddWithValue("@IDstore", IDstore);
                    Object obj = cmdSearch.ExecuteScalar();
                    // MySqlDataReader reader = cmdSearch.ExecuteReader();
                    //reader.Read();
                    if (obj == null || obj == DBNull.Value)
                    {
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = ConnectionWithDB.GetConnection();
                        cmd.CommandText = "INSERT INTO stocks VALUES(@IDnom, @IDstore, @count)";
                        cmd.Parameters.AddWithValue("@IDnom", IDnom);
                        cmd.Parameters.AddWithValue("@IDstore", IDstore);
                        cmd.Parameters.AddWithValue("@count", count);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Запрос выполнен.");
                    }
                    else
                    {
                        int sum = Convert.ToInt32(obj.ToString()) + Convert.ToInt32(count);
                        MySqlCommand cmdUpdate = new MySqlCommand();
                        cmdUpdate.Connection = ConnectionWithDB.GetConnection();
                        cmdUpdate.CommandText = "UPDATE stocks SET St_Count = @count WHERE Nom_ID = @IDnom AND Storage_ID = @IDstore";
                        cmdUpdate.Parameters.AddWithValue("@count", sum);
                        cmdUpdate.Parameters.AddWithValue("@IDnom", IDnom);
                        cmdUpdate.Parameters.AddWithValue("@IDstore", IDstore);
                        cmdUpdate.ExecuteNonQuery();
                        MessageBox.Show("Запрос выполнен.");

                    }
                    ConnectionWithDB.CloseConnection();
                }
                catch
                {
                    MessageBox.Show("Нет соединения с базой данных:(");
                }
            }
            else
            {
                MessageBox.Show("В строку количество введите целое число.");
            }
        }
    }
}
