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
    /// Логика взаимодействия для Usage_log.xaml
    /// </summary>
    public partial class Usage_log : Window
    {
        public Usage_log()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectionWithDB.OpenConnection();
            MySqlCommand cmdDG = new MySqlCommand();
            cmdDG.CommandText = "SELECT * FROM ordering WHERE Ord_Status != 'Выполнен'";
            cmdDG.Connection = ConnectionWithDB.GetConnection();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdDG);
            DataTable dt = new DataTable("ordering");

            da.Fill(dt);
            datagrid.ItemsSource = dt.DefaultView;
            ConnectionWithDB.CloseConnection();
            datagrid.Columns[0].Header = "ID заказа";
            datagrid.Columns[0].IsReadOnly = true;
            datagrid.Columns[1].Header = "Статус заказа";
            datagrid.Columns[1].IsReadOnly = true;
            datagrid.Columns[2].Header = "Количество";
            datagrid.Columns[2].IsReadOnly = true;
            datagrid.Columns[3].Header = "Дата создания заказа";
            datagrid.Columns[3].IsReadOnly = true;
            datagrid.Columns[4].Header = "Плановая дата выполнения";
            datagrid.Columns[4].IsReadOnly = true;
            datagrid.Columns[5].Header = "ID номенклатуры";
            datagrid.Columns[5].IsReadOnly = true;

        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            string Ord_ID = ((DataRowView)datagrid.SelectedItems[0]).Row["Ord_ID"].ToString();
            ConnectionWithDB.OpenConnection();
            /*MySqlCommand cmdIDnom = new MySqlCommand();
            cmdIDnom.CommandText = "SELECT Nom_ID FROM ordering WHERE Ord_ID = @Ord_ID";
            cmdIDnom.Connection = ConnectionWithDB.GetConnection();
            cmdIDnom.Parameters.AddWithValue("@Ord_ID", Ord_ID);
            Object obj = cmdIDnom.ExecuteScalar();
            int Nom_ID = Convert.ToInt32(obj.ToString());

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT Map_ID FROM nomenclature WHERE Nom_ID = @Nom_ID";
            cmd.Connection = ConnectionWithDB.GetConnection();
            cmd.Parameters.AddWithValue("@Nom_ID", Nom_ID);
            Object objMapID = cmd.ExecuteScalar();
            int Map_ID = Convert.ToInt32(objMapID.ToString());*/

                MySqlCommand cmdMap = new MySqlCommand();
                cmdMap.CommandText = "SELECT * FROM work_log WHERE Ord_ID = @Ord_ID";
                cmdMap.Parameters.AddWithValue("@Ord_ID", Convert.ToInt32(Ord_ID));
                cmdMap.Connection = ConnectionWithDB.GetConnection();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdMap);
                DataTable dt = new DataTable("work_log");
                da.Fill(dt);
                datagridMap.ItemsSource = dt.DefaultView;
                datagridMap.Columns[0].Header = "ID записи";
                datagridMap.Columns[0].IsReadOnly = true;
                datagridMap.Columns[1].Header = "Время на сборку";
                datagridMap.Columns[1].IsReadOnly = true;
                datagridMap.Columns[2].Header = "ID заказа";
                datagridMap.Columns[2].IsReadOnly = true;
                datagridMap.Columns[3].Header = "ID номенклатуры";
                datagridMap.Columns[3].IsReadOnly = true;
                datagridMap.Columns[4].Header = "ID операции";
                datagridMap.Columns[4].IsReadOnly = true;
            datagridMap.Columns[5].Header = "ID тех. карты";
            datagridMap.Columns[5].IsReadOnly = true;

            MySqlCommand cmdUse = new MySqlCommand();
            cmdUse.CommandText = "SELECT * FROM usage_log WHERE Ord_ID = @Ord_ID";
            cmdUse.Parameters.AddWithValue("@Ord_ID", Convert.ToInt32(Ord_ID));
            cmdUse.Connection = ConnectionWithDB.GetConnection();
            MySqlDataAdapter daU = new MySqlDataAdapter(cmdUse);
            DataTable dtU = new DataTable("usage_log");
            daU.Fill(dtU);
            datagridUse.ItemsSource = dtU.DefaultView;
            datagridUse.Columns[0].Header = "ID записи";
            datagridUse.Columns[0].IsReadOnly = true;
            datagridUse.Columns[1].Header = "Количество";
            datagridUse.Columns[1].IsReadOnly = true;
            datagridUse.Columns[2].Header = "ID заказа";
            datagridUse.Columns[2].IsReadOnly = true;
            datagridUse.Columns[3].Header = "ID номенклатуры";
            datagridUse.Columns[3].IsReadOnly = true;

            ConnectionWithDB.CloseConnection();
        }

        private void Button_Click_WriteOff(object sender, RoutedEventArgs e)
        {
            string Ord_ID = ((DataRowView)datagrid.SelectedItems[0]).Row["Ord_ID"].ToString();
            var errorWC = new List<string>();
            var unavailableWC = new List<string>();
            var notCountNom = new List<string>();
            ConnectionWithDB.OpenConnection();

            MySqlCommand cmdTime = new MySqlCommand();
            cmdTime.CommandText = "SELECT Log_Work_Time FROM work_log WHERE Ord_ID = @Ord_ID ";
            cmdTime.Parameters.AddWithValue("@Ord_ID", Ord_ID);
            cmdTime.Connection = ConnectionWithDB.GetConnection();

            MySqlDataReader reader = cmdTime.ExecuteReader();
            var Timelist = new List<string>();
            while (reader.Read())
            {
                Timelist.Add(reader[0].ToString());
            }
            reader.Close();
            MySqlCommand cmdMapID = new MySqlCommand();
            cmdMapID.CommandText = "SELECT Map_ID FROM work_log WHERE Ord_ID = @Ord_ID ";
            cmdMapID.Parameters.AddWithValue("@Ord_ID", Ord_ID);
            cmdMapID.Connection = ConnectionWithDB.GetConnection();
            MySqlDataReader readerMapID = cmdMapID.ExecuteReader();
            var MapIDlist = new List<string>();
            while (readerMapID.Read())
            {
                MapIDlist.Add(readerMapID[0].ToString());
            }
            readerMapID.Close();
            var WCIDlist = new List<string>();
            for (int i = 0; i < MapIDlist.Count; i++)
            {
                MySqlCommand cmdWCID = new MySqlCommand();
                cmdWCID.CommandText = "SELECT WC_ID FROM map_composition WHERE Map_ID = @Map_ID ";
                cmdWCID.Parameters.AddWithValue("@Map_ID", MapIDlist[i]);
                cmdWCID.Connection = ConnectionWithDB.GetConnection();
                Object objWCID = cmdWCID.ExecuteScalar();
                WCIDlist.Add(objWCID.ToString());
            }

            for (int j = 0; j < WCIDlist.Count; j++)
            {
                MySqlCommand cmdWCCategory = new MySqlCommand();
                cmdWCCategory.CommandText = "SELECT WC_status FROM work_center WHERE WC_ID = @WC_ID ";
                cmdWCCategory.Parameters.AddWithValue("@WC_ID", WCIDlist[j]);
                cmdWCCategory.Connection = ConnectionWithDB.GetConnection();
                Object objWCCategory = cmdWCCategory.ExecuteScalar();

                if (objWCCategory.ToString() == "Доступен")
                {

                    MySqlCommand cmdWCTime = new MySqlCommand();
                    cmdWCTime.CommandText = "SELECT WC_Work_Time FROM work_center WHERE WC_ID = @WC_ID ";
                    cmdWCTime.Parameters.AddWithValue("@WC_ID", WCIDlist[j]);
                    cmdWCTime.Connection = ConnectionWithDB.GetConnection();
                    Object objWCTime = cmdWCTime.ExecuteScalar();

                    if (Convert.ToInt32(objWCTime) > Convert.ToInt32(Timelist[j]))
                    {
                        int time = (Convert.ToInt32(objWCTime) - Convert.ToInt32(Timelist[j]));

                        MySqlCommand cmdUpdate = new MySqlCommand();
                        cmdUpdate.CommandText = "UPDATE work_center SET WC_Work_Time = @time WHERE WC_ID = @WC_IDlist";
                        cmdUpdate.Parameters.AddWithValue("@time", time);
                        cmdUpdate.Parameters.AddWithValue("@WC_IDlist", Convert.ToInt32(WCIDlist[j]));
                        cmdUpdate.Connection = ConnectionWithDB.GetConnection();
                        cmdUpdate.ExecuteNonQuery();

                        MySqlCommand cmdUpdateLog = new MySqlCommand();
                        cmdUpdateLog.CommandText = "UPDATE work_log SET Log_Work_Time = 0 WHERE Ord_ID = @Ord_ID AND Map_ID = @MapIDlist";
                        cmdUpdateLog.Parameters.AddWithValue("@Ord_ID", Ord_ID);
                        cmdUpdateLog.Parameters.AddWithValue("@MapIDlist", MapIDlist[j]);
                        cmdUpdateLog.Connection = ConnectionWithDB.GetConnection();
                        cmdUpdateLog.ExecuteNonQuery();

                        MySqlCommand cmdUpdStatus = new MySqlCommand();
                        cmdUpdStatus.CommandText = "UPDATE ordering SET Ord_status = 'В процессе' WHERE Ord_ID = @Ord_IDUPD";
                        cmdUpdStatus.Parameters.AddWithValue("@Ord_IDUPD", Ord_ID);
                        cmdUpdStatus.Connection = ConnectionWithDB.GetConnection();
                        cmdUpdStatus.ExecuteNonQuery();
                    }
                    else
                    {
                        errorWC.Add(WCIDlist[j]);
                    }
                }
                else
                {
                    unavailableWC.Add(WCIDlist[j]);
                }
            }



            MySqlCommand cmdCount = new MySqlCommand();
            cmdCount.CommandText = "SELECT Log_Count FROM usage_log WHERE Ord_ID = @Ord_ID ";
            cmdCount.Parameters.AddWithValue("@Ord_ID", Ord_ID);
            cmdCount.Connection = ConnectionWithDB.GetConnection();

            MySqlDataReader readerCount = cmdCount.ExecuteReader();
            var Countlist = new List<string>();
            while (readerCount.Read())
            {
                Countlist.Add(readerCount[0].ToString());
            }
            readerCount.Close();

            MySqlCommand cmdNomID = new MySqlCommand();
            cmdNomID.CommandText = "SELECT Nom_ID FROM usage_log WHERE Ord_ID = @Ord_ID ";
            cmdNomID.Parameters.AddWithValue("@Ord_ID", Ord_ID);
            cmdNomID.Connection = ConnectionWithDB.GetConnection();

            MySqlDataReader readerNomID = cmdNomID.ExecuteReader();
            var NomIDlist = new List<string>();
            while (readerNomID.Read())
            {
                NomIDlist.Add(readerNomID[0].ToString());
            }
            readerNomID.Close();

            for(int k = 0; k < NomIDlist.Count; k++)
            {
                MySqlCommand cmdNomCount = new MySqlCommand();
                cmdNomCount.CommandText = "SELECT St_Count FROM stocks WHERE Nom_ID = @Nom_ID ";
                cmdNomCount.Parameters.AddWithValue("@Nom_ID", NomIDlist[k]);
                cmdNomCount.Connection = ConnectionWithDB.GetConnection();
                Object objNomCount = cmdNomCount.ExecuteScalar();
                if(Convert.ToInt32(objNomCount) > Convert.ToInt32(Countlist[k]))
                {
                    int count = (Convert.ToInt32(objNomCount) - Convert.ToInt32(Countlist[k]));

                    MySqlCommand cmdUpdate = new MySqlCommand();
                    cmdUpdate.CommandText = "UPDATE stocks SET St_Count = @count WHERE Nom_ID = @NomIDlist";
                    cmdUpdate.Parameters.AddWithValue("@count", count);
                    cmdUpdate.Parameters.AddWithValue("@NomIDlist", Convert.ToInt32(NomIDlist[k]));
                    cmdUpdate.Connection = ConnectionWithDB.GetConnection();
                    cmdUpdate.ExecuteNonQuery();

                    MySqlCommand cmdUpdateLog = new MySqlCommand();
                    cmdUpdateLog.CommandText = "UPDATE usage_log SET Log_Count = 0 WHERE Ord_ID = @Ord_ID AND Nom_ID = @NomIDlist";
                    cmdUpdateLog.Parameters.AddWithValue("@Ord_ID", Ord_ID);
                    cmdUpdateLog.Parameters.AddWithValue("@NomIDlist", NomIDlist[k]);
                    cmdUpdateLog.Connection = ConnectionWithDB.GetConnection();
                    cmdUpdateLog.ExecuteNonQuery();
                }
                else
                {
                    notCountNom.Add(NomIDlist[k]);
                }
            }

            MySqlCommand cmdCheckCount = new MySqlCommand();
            cmdCheckCount.CommandText = "SELECT Log_Count FROM usage_log WHERE Log_Count != 0";
            cmdCheckCount.Connection = ConnectionWithDB.GetConnection();
            Object objResCount = cmdCheckCount.ExecuteScalar();

            MySqlCommand cmdCheckTime = new MySqlCommand();
            cmdCheckTime.CommandText = "SELECT WC_Work_Time FROM work_center WHERE WC_Work_Time != 0";
            cmdCheckTime.Connection = ConnectionWithDB.GetConnection();
            Object objResTime = cmdCheckCount.ExecuteScalar();

            if(objResCount == null && objResCount == DBNull.Value && objResTime == null && objResTime == DBNull.Value)
            {
                MySqlCommand cmdUpdStatus = new MySqlCommand();
                cmdUpdStatus.CommandText = "UPDATE ordering SET Ord_status = 'Выполнен' WHERE Ord_ID = @Ord_IDUPD";
                cmdUpdStatus.Parameters.AddWithValue("@Ord_IDUPD", Ord_ID);
                cmdUpdStatus.Connection = ConnectionWithDB.GetConnection();
                cmdUpdStatus.ExecuteNonQuery();
                MessageBox.Show("Заказ выполнен.");
                datagridMap.Items.Clear();
                datagridUse.Items.Clear();


            }
            else
            {
                MySqlCommand cmdUpdStatus = new MySqlCommand();
                cmdUpdStatus.CommandText = "UPDATE ordering SET Ord_status = 'В процессе' WHERE Ord_ID = @Ord_IDUPD";
                cmdUpdStatus.Parameters.AddWithValue("@Ord_IDUPD", Ord_ID);
                cmdUpdStatus.Connection = ConnectionWithDB.GetConnection();
                cmdUpdStatus.ExecuteNonQuery();
            }


            string delimiter = ", ";
            string messageBoxContent = "На рабочих центрах с ID = " + String.Join(delimiter, errorWC) + " не хватает ресурсов для списания";
            string messageBoxContentTwo = "На данный момент рабочие центры с ID = " + String.Join(delimiter, unavailableWC) + " не доступны";
            string messageBoxContentThree = "Номенклатуры с ID = " + String.Join(delimiter, notCountNom) + " закончились на складе";
            MessageBox.Show(messageBoxContent + Environment.NewLine + messageBoxContentTwo + Environment.NewLine + messageBoxContentThree);

            MySqlCommand cmdDG = new MySqlCommand();
            cmdDG.CommandText = "SELECT * FROM ordering WHERE Ord_Status != 'Выполнен'";
            cmdDG.Connection = ConnectionWithDB.GetConnection();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdDG);
            DataTable dt = new DataTable("ordering");

            da.Fill(dt);
            datagrid.ItemsSource = dt.DefaultView;
            ConnectionWithDB.CloseConnection();
            datagrid.Columns[0].Header = "ID заказа";
            datagrid.Columns[0].IsReadOnly = true;
            datagrid.Columns[1].Header = "Статус заказа";
            datagrid.Columns[1].IsReadOnly = true;
            datagrid.Columns[2].Header = "Количество";
            datagrid.Columns[2].IsReadOnly = true;
            datagrid.Columns[3].Header = "Дата создания заказа";
            datagrid.Columns[3].IsReadOnly = true;
            datagrid.Columns[4].Header = "Плановая дата выполнения";
            datagrid.Columns[4].IsReadOnly = true;
            datagrid.Columns[5].Header = "ID номенклатуры";
            datagrid.Columns[5].IsReadOnly = true;
            
            ConnectionWithDB.CloseConnection();
        }
    }
}
