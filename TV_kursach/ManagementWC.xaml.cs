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
    /// Логика взаимодействия для ManagementWC.xaml
    /// </summary>
    public partial class ManagementWC : Window
    {
        public ManagementWC()
        {
            InitializeComponent();
            try
            {
                ConnectionWithDB.OpenConnection();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = ConnectionWithDB.GetConnection();
                cmd.CommandText = "SELECT WC_ID FROM work_center";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboboxIDWC.Items.Add(reader[0].ToString());
                }
                ConnectionWithDB.CloseConnection();
                comboboxStatus.Items.Add("Доступен");
                comboboxStatus.Items.Add("Недоступен");
            }
            catch
            {
                MessageBox.Show("Нет соединения с базой данных:(");
            }
        }
        
        private void comboboxStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboboxStatus.SelectedValue.ToString() == "Доступен")
            {
                textboxTimeWork.IsReadOnly = false;
            }
            else
            {
                textboxTimeWork.IsReadOnly = true;
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
        
            int IDWC = Convert.ToInt32(comboboxIDWC.SelectedValue.ToString());
            string status = comboboxStatus.SelectedValue.ToString();
            string timework = textboxTimeWork.Text;
            int num;
            bool isnum = int.TryParse(timework, out num);
            if (isnum || status == "Недоступен")
            {
                if (status == "Недоступен")
                {
                    textboxTimeWork.Text = "";
                    timework = "";
                }
                try
                {
                    ConnectionWithDB.OpenConnection();
                    MySqlCommand cmdSearch = new MySqlCommand();
                    cmdSearch.Connection = ConnectionWithDB.GetConnection();
                    cmdSearch.CommandText = "SELECT WC_ID FROM work_center WHERE WC_ID = @IDWC";
                    cmdSearch.Parameters.AddWithValue("@IDWC", IDWC);
                    Object obj = cmdSearch.ExecuteScalar();
                    if (obj == null && obj == DBNull.Value)
                    {
                        if (status == "Доступен")
                        {

                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = ConnectionWithDB.GetConnection();
                            cmd.CommandText = "INSERT INTO work_center VALUES(@IDWC, @status, @timework)";
                            cmd.Parameters.AddWithValue("@IDWC", IDWC);
                            cmd.Parameters.AddWithValue("@status", status);
                            cmd.Parameters.AddWithValue("@timework", timework);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Запрос выполнен.");
                        }
                        else
                        {
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = ConnectionWithDB.GetConnection();
                            cmd.CommandText = "INSERT INTO work_center VALUES(@IDWC, @status)";
                            cmd.Parameters.AddWithValue("@IDWC", IDWC);
                            cmd.Parameters.AddWithValue("@status", status);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Запрос выполнен.");
                        }
                    }
                    else
                    {
                        MySqlCommand cmdUpdate = new MySqlCommand();
                        cmdUpdate.Connection = ConnectionWithDB.GetConnection();
                        cmdUpdate.CommandText = "UPDATE work_center SET WC_Work_Time = @timework, WC_status = @status WHERE WC_ID = @IDWC";
                        cmdUpdate.Parameters.AddWithValue("@timework", timework);
                        cmdUpdate.Parameters.AddWithValue("@status", status);
                        cmdUpdate.Parameters.AddWithValue("@IDWC", IDWC);
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
                MessageBox.Show("В строку время работы введите целое число.");
            }
        }
    }
}
