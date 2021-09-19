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
    /// Логика взаимодействия для Decommissioning.xaml
    /// </summary>
    public partial class Decommissioning : Window
    {
        public Decommissioning()
        {
            InitializeComponent();
            try
            {
                ConnectionWithDB.OpenConnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = ConnectionWithDB.GetConnection();
                cmd.CommandText = "SELECT Storage_ID FROM stocks";
                MySqlDataReader reader = cmd.ExecuteReader();
                string str = "";
                var strResult = string.Empty;
                while (reader.Read())
                {
                    //comboboxIDStore.Items.Add(reader[0].ToString()); ;
                    str += reader[0].ToString();
                }
                foreach (var element in str.ToCharArray())
                {
                    if (strResult.Length == 0 || strResult[strResult.Length - 1] != element)
                        strResult = $"{strResult}{element}";
                }
                foreach (var element in strResult.ToCharArray())
                {
                    comboboxIDStore.Items.Add(element);
                }
                var mas = new List<string>();

                strResult = "";
                reader.Close();
                MySqlCommand cmdSec = new MySqlCommand();
                cmdSec.Connection = ConnectionWithDB.GetConnection();
                cmdSec.CommandText = "SELECT Nom_ID FROM stocks";
                MySqlDataReader readerSec = cmdSec.ExecuteReader();
                while (readerSec.Read())
                {
                    //comboboxIDStore.Items.Add(reader[0].ToString()); ;
                    mas.Add(readerSec[0].ToString());
                }
                var res = mas.Distinct().ToList();
                foreach (var element in res)
                {
                    comboboxIDnom.Items.Add(element);
                }
                ConnectionWithDB.CloseConnection();
            }
            catch
            {
                MessageBox.Show("Нет соединения с базой данных:(");
            }
        }

        private void Button_Click_Execute(object sender, RoutedEventArgs e)
        {
            int num;
            bool isnum = int.TryParse(textboxCount.Text, out num);
            if (isnum)
            {
                try
                {
                    int IDNom = Convert.ToInt32(comboboxIDnom.SelectedValue.ToString());
                    int IDStore = Convert.ToInt32(comboboxIDStore.SelectedValue.ToString());
                    int count = Convert.ToInt32(textboxCount.Text);
                    ConnectionWithDB.OpenConnection();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = ConnectionWithDB.GetConnection();
                    cmd.CommandText = "SELECT St_Count FROM stocks WHERE Nom_ID = @IDNom AND Storage_ID = @IDstore";
                    cmd.Parameters.AddWithValue("IDNom", IDNom);
                    cmd.Parameters.AddWithValue("IDStore", IDStore);
                    Object obj = cmd.ExecuteScalar();
                    if (Convert.ToInt32(obj) > count || Convert.ToInt32(obj) == count)
                    {
                        int sum = Convert.ToInt32(obj.ToString()) - count;
                        MySqlCommand cmdUpdate = new MySqlCommand();
                        cmdUpdate.Connection = ConnectionWithDB.GetConnection();
                        cmdUpdate.CommandText = "UPDATE stocks SET St_Count = @count WHERE Nom_ID = @IDnom AND Storage_ID = @IDstore ";
                        cmdUpdate.Parameters.AddWithValue("@count", sum);
                        cmdUpdate.Parameters.AddWithValue("@IDnom", IDNom);
                        cmdUpdate.Parameters.AddWithValue("@IDstore", IDStore);
                        cmdUpdate.ExecuteNonQuery();
                        MessageBox.Show("Запрос выполнен.");
                    }
                    else
                    {
                        MessageBox.Show("Невозможно списать больше материалов, чем есть на складе. Изменить количество.");
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
