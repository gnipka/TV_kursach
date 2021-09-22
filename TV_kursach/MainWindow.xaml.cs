using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using MySql.Data.MySqlClient;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace TV_kursach
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string SelectedValue { get; private set; }

        private void RadioButtonOrdering_Checked(object sender, RoutedEventArgs e)
        {
            ConnectionWithDB.OpenConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM ordering";
            cmd.Connection = ConnectionWithDB.GetConnection();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable("ordering");
            da.Fill(dt);
            dataGrid.ItemsSource = dt.DefaultView;
            ConnectionWithDB.CloseConnection();
            dataGrid.Columns[0].Header = "ID заказа";
            dataGrid.Columns[1].Header = "Статус заказа";
            dataGrid.Columns[2].Header = "Количество";
            dataGrid.Columns[3].Header = "Дата создания заказа";
            dataGrid.Columns[4].Header = "Плановая дата выполнения";
            dataGrid.Columns[5].Header = "ID номенклатуры";
            if (sender is RadioButton item)
            {
                SelectedValue = item.Content.ToString();
            }
        }

        private void RadioButtonNom_Checked(object sender, RoutedEventArgs e)
        {
            ConnectionWithDB.OpenConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM nomenclature";
            cmd.Connection = ConnectionWithDB.GetConnection();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable("nomenclature");
            da.Fill(dt);
            dataGrid.ItemsSource = dt.DefaultView;
            ConnectionWithDB.CloseConnection();
            dataGrid.Columns[0].Header = "ID номенклатуры";
            dataGrid.Columns[1].Header = "Описание";
            dataGrid.Columns[2].Header = "ID технологической карты";
            dataGrid.Columns[3].Header = "ID спецификатора";
            if (sender is RadioButton item)
            {
                SelectedValue = item.Content.ToString();
            }
        }

        private void RadioButtonStorage_Checked(object sender, RoutedEventArgs e)
        {
            ConnectionWithDB.OpenConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM storage";
            cmd.Connection = ConnectionWithDB.GetConnection();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable("storage");
            da.Fill(dt);
            dataGrid.ItemsSource = dt.DefaultView;
            ConnectionWithDB.CloseConnection();
            dataGrid.Columns[0].Header = "ID склада";
            dataGrid.Columns[1].Header = "Описание";
            if (sender is RadioButton item)
            {
                SelectedValue = item.Content.ToString();
            }
        }

        private void RadioButtonWC_Checked(object sender, RoutedEventArgs e)
        {
            ConnectionWithDB.OpenConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM work_center";
            cmd.Connection = ConnectionWithDB.GetConnection();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable("work_center");
            da.Fill(dt);
            dataGrid.ItemsSource = dt.DefaultView;
            ConnectionWithDB.CloseConnection();
            dataGrid.Columns[0].Header = "ID Рабочего центра";
            dataGrid.Columns[1].Header = "Описание";
            dataGrid.Columns[2].Header = "Время работы";
            if (sender is RadioButton item)
            {
                SelectedValue = item.Content.ToString();
            }
        }

        private void RadioButtonStocks_Checked(object sender, RoutedEventArgs e)
        {
            ConnectionWithDB.OpenConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM stocks";
            cmd.Connection = ConnectionWithDB.GetConnection();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable("stocks");
            da.Fill(dt);
            dataGrid.ItemsSource = dt.DefaultView;
            ConnectionWithDB.CloseConnection();
            dataGrid.Columns[0].Header = "ID номенклатуры";
            dataGrid.Columns[1].Header = "ID склада";
            dataGrid.Columns[2].Header = "Количество";
            if (sender is RadioButton item)
            {
                SelectedValue = item.Content.ToString();
            }
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            AddData addData = new AddData(SelectedValue);
            addData.ShowDialog();
        }

        private void Button_Click_OpenPurchaseOrder(object sender, RoutedEventArgs e)
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            purchaseOrder.Show();

        }

        private void Button_Click_OpenManagementWC(object sender, RoutedEventArgs e)
        {
            ManagementWC managementWC = new ManagementWC();
            managementWC.Show();
        }

        private void Button_Click_OpenDecommissioning(object sender, RoutedEventArgs e)
        {
            Decommissioning decommissioning = new Decommissioning();
            decommissioning.Show();
        }

        private void Button_Click_OpenOrder(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            order.Show();
        }

        private void Button_Click_Open_Usage_log(object sender, RoutedEventArgs e)
        {
            Usage_log usage_Log = new Usage_log();
            usage_Log.Show();
        }
    }
}
