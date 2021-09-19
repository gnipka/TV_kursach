using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
            reader.Close();
            MySqlCommand cmdID = new MySqlCommand();
            cmdID.Connection = ConnectionWithDB.GetConnection();
            cmdID.CommandText = "SELECT Ord_ID FROM ordering";
            MySqlDataReader readerID = cmdID.ExecuteReader();
            while (readerID.Read())
            {
                comboboxIDOrder.Items.Add(readerID[0].ToString()); ;
            }
            readerID.Close();
            ConnectionWithDB.CloseConnection();
            comboboxStatus.Items.Add("В разработке");
            comboboxStatus.Items.Add("Выполнен");
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
                string Ord_Status = "Принят";
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
                MySqlCommand cmdDG = new MySqlCommand();
                cmdDG.CommandText = "SELECT * FROM ordering";
                cmdDG.Connection = ConnectionWithDB.GetConnection();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdDG);
                DataTable dt = new DataTable("ordering");

                da.Fill(dt);
                datagrid.ItemsSource = dt.DefaultView;
                comboboxIDOrder.Items.Clear();
                MySqlCommand cmdID = new MySqlCommand();
                cmdID.Connection = ConnectionWithDB.GetConnection();
                cmdID.CommandText = "SELECT Ord_ID FROM ordering";
                MySqlDataReader readerID = cmdID.ExecuteReader();
                while (readerID.Read())
                {
                    comboboxIDOrder.Items.Add(readerID[0].ToString()); ;
                }
                readerID.Close();
                ConnectionWithDB.CloseConnection();
                datagrid.Columns[0].Header = "ID заказа";
                datagrid.Columns[1].Header = "Статус заказа";
                datagrid.Columns[2].Header = "Количество";
                datagrid.Columns[3].Header = "Дата создания заказа";
                datagrid.Columns[4].Header = "Плановая дата выполнения";
                datagrid.Columns[5].Header = "ID номенклатуры";
                MessageBox.Show("Запрос выполнен.");
            }
            catch
            {
                MessageBox.Show("Нет соединения с базой данных:(");
            }

        }

        private void Button_Click_ChangeStatus(object sender, RoutedEventArgs e)
        {
            int Ord_ID = Convert.ToInt32(comboboxIDOrder.SelectedValue.ToString());
            string status = comboboxStatus.SelectedValue.ToString();
            ConnectionWithDB.OpenConnection();
            MySqlCommand cmdUpdate = new MySqlCommand();
            cmdUpdate.Connection = ConnectionWithDB.GetConnection();
            cmdUpdate.CommandText = "UPDATE ordering SET Ord_Status = @status WHERE Ord_ID = @Ord_ID";
            cmdUpdate.Parameters.AddWithValue("@status", status);
            cmdUpdate.Parameters.AddWithValue("@Ord_ID", Ord_ID);
            cmdUpdate.ExecuteNonQuery();
            MySqlCommand cmdDG = new MySqlCommand();
            cmdDG.CommandText = "SELECT * FROM ordering";
            cmdDG.Connection = ConnectionWithDB.GetConnection();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdDG);
            DataTable dt = new DataTable("ordering");

            da.Fill(dt);
            datagrid.ItemsSource = dt.DefaultView;

            ConnectionWithDB.CloseConnection();

            MessageBox.Show("Запрос выполнен.");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectionWithDB.OpenConnection();
            MySqlCommand cmdDG = new MySqlCommand();
            cmdDG.CommandText = "SELECT * FROM ordering";
            cmdDG.Connection = ConnectionWithDB.GetConnection();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdDG);
            DataTable dt = new DataTable("ordering");

            da.Fill(dt);
            datagrid.ItemsSource = dt.DefaultView;
            ConnectionWithDB.CloseConnection();
            datagrid.Columns[0].Header = "ID заказа";
            datagrid.Columns[1].Header = "Статус заказа";
            datagrid.Columns[2].Header = "Количество";
            datagrid.Columns[3].Header = "Дата создания заказа";
            datagrid.Columns[4].Header = "Плановая дата выполнения";
            datagrid.Columns[5].Header = "ID номенклатуры";
        }
    }
}
