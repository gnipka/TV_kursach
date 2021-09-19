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
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        public Order()
        {
            InitializeComponent();
            ConnectionWithDB.OpenConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = ConnectionWithDB.GetConnection();
            cmd.CommandText = "SELECT Nom_Description FROM nomenclature WHERE Map_ID IS NOT NULL";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboboxNom.Items.Add(reader[0].ToString()); ;
            }
            ConnectionWithDB.CloseConnection();
        }

        private void Button_Click_MakeAnOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                string Nom_Description = comboboxNom.SelectedValue.ToString();
                ConnectionWithDB.OpenConnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = ConnectionWithDB.GetConnection();
                cmd.CommandText = "SELECT Nom_ID FROM nomenclature WHERE Nom_Description = @Nom_Description";
                cmd.Parameters.AddWithValue("@Nom_Description", Nom_Description);
                Object obj = cmd.ExecuteScalar();
                int Nom_ID = Convert.ToInt32(obj);
                string Ord_Status = "В процессе";
                int Ord_Count = Convert.ToInt32(textboxCount.Text);
                string Ord_Date = DateTime.Now.ToString("dd MMMM yyyy");
                MySqlCommand cmdAdd = new MySqlCommand();
                cmdAdd.Connection = ConnectionWithDB.GetConnection();
                cmdAdd.CommandText = "INSERT INTO ordering (Ord_Status, Ord_Count, Ord_Date, Nom_ID) VALUES (@Ord_Status, @Ord_Count, @Ord_Date, @Nom_ID)";
                cmdAdd.Parameters.AddWithValue("@Ord_Status", Ord_Status);
                cmdAdd.Parameters.AddWithValue("@Ord_Count", Ord_Count);
                cmdAdd.Parameters.AddWithValue("@Ord_Date", Ord_Date);
                cmdAdd.Parameters.AddWithValue("@Nom_ID", Nom_ID);
                cmdAdd.ExecuteNonQuery();
                ConnectionWithDB.CloseConnection();
            }
            catch
            {
                MessageBox.Show("Нет соединения с базой данных:(");
            }
        }
    }
}
