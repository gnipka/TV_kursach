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
            comboboxStatus.Items.Add("В процессе");
            comboboxStatus.Items.Add("Выполнен");
        }

        private void Button_Click_MakeAnOrder(object sender, RoutedEventArgs e)
        {
          //  try
            //{
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
            MessageBox.Show("Запрос выполнен.");

                MySqlCommand cmdMaxID = new MySqlCommand();
                cmdMaxID.Connection = ConnectionWithDB.GetConnection();
                cmdMaxID.CommandText = "SELECT MAX( Ord_ID ) FROM ordering";
                Object objMaxID = cmdMaxID.ExecuteScalar();
                int Ord_ID_WL = Convert.ToInt32(objMaxID.ToString());
                MySqlCommand cmdNomID = new MySqlCommand();
                cmdNomID.Connection = ConnectionWithDB.GetConnection();
                cmdNomID.CommandText = "SELECT Nom_ID FROM ordering WHERE Ord_ID = @Ord_ID_WL";
                cmdNomID.Parameters.AddWithValue("@Ord_ID_WL", Ord_ID_WL);
                Object objNom_ID = cmdNomID.ExecuteScalar();
                int Nom_ID_WL = Convert.ToInt32(objNom_ID.ToString());
                MySqlCommand cmdMap_ID = new MySqlCommand();
                cmdMap_ID.Connection = ConnectionWithDB.GetConnection();
                cmdMap_ID.CommandText = "SELECT Map_ID FROM nomenclature WHERE Nom_ID = @Nom_ID_WL";
                cmdMap_ID.Parameters.AddWithValue("@Nom_ID_WL", Nom_ID_WL);
                Object objMap_ID = cmdMap_ID.ExecuteScalar();
                int Map_ID_WL = Convert.ToInt32(objMap_ID.ToString());
                MySqlCommand cmdWork_Time = new MySqlCommand();
                cmdWork_Time.Connection = ConnectionWithDB.GetConnection();
                cmdWork_Time.CommandText = "SELECT Processing_Time FROM map_composition WHERE Map_ID = @Map_ID_WL";
                cmdWork_Time.Parameters.AddWithValue("@Map_ID_WL", Map_ID_WL);
                Object objWork_Time = cmdWork_Time.ExecuteScalar();
                int Log_Work_Time_WL = Convert.ToInt32(objWork_Time.ToString());
                MySqlCommand cmdOperation_ID = new MySqlCommand();
                cmdOperation_ID.Connection = ConnectionWithDB.GetConnection();
                cmdOperation_ID.CommandText = "SELECT Operation_ID FROM map_composition WHERE Map_ID = @Map_ID_WL";
                cmdOperation_ID.Parameters.AddWithValue("@Map_ID_WL", Map_ID_WL);
                Object objOperation_ID = cmdOperation_ID.ExecuteScalar();
                int Operation_ID_WL = Convert.ToInt32(objOperation_ID.ToString());

            MySqlCommand cmdCount = new MySqlCommand();
            cmdCount.Connection = ConnectionWithDB.GetConnection();
            cmdCount.CommandText = "SELECT Ord_Count FROM ordering WHERE Ord_ID = @Ord_ID_WL";
            cmdCount.Parameters.AddWithValue("@Ord_ID_WL", Ord_ID_WL);
            Object objCount = cmdCount.ExecuteScalar();
            int Count_UL = Convert.ToInt32(objCount.ToString());

            if (Nom_ID_WL == 18)
            {
                int countA = (Count_UL * 50);
                MySqlCommand cmdAddinWL = new MySqlCommand();
                cmdAddinWL.Connection = ConnectionWithDB.GetConnection();
                cmdAddinWL.CommandText = "INSERT INTO work_log (Log_Work_Time, Ord_ID, Nom_ID, Operation_ID, Map_ID) VALUES (@countA, @Ord_ID_WL, '14', '1', '1')";
                cmdAddinWL.Parameters.AddWithValue("@countA", countA);
                cmdAddinWL.Parameters.AddWithValue("@Ord_ID_WL", Ord_ID_WL);
                cmdAddinWL.ExecuteScalar();

                int countB = (Count_UL * 15);
                MySqlCommand cmdAddinWLOne = new MySqlCommand();
                cmdAddinWLOne.Connection = ConnectionWithDB.GetConnection();
                cmdAddinWLOne.CommandText = "INSERT INTO work_log (Log_Work_Time, Ord_ID, Nom_ID, Operation_ID, Map_ID) VALUES (@countB, @Ord_ID_WLOne, '15', '2', '2')";
                cmdAddinWLOne.Parameters.AddWithValue("@countB", countB);
                cmdAddinWLOne.Parameters.AddWithValue("@Ord_ID_WLOne", Ord_ID_WL);
                cmdAddinWLOne.ExecuteScalar();

                int countC = (Count_UL * 100);
                MySqlCommand cmdAddinWLTwo = new MySqlCommand();
                cmdAddinWLTwo.Connection = ConnectionWithDB.GetConnection();
                cmdAddinWLTwo.CommandText = "INSERT INTO work_log (Log_Work_Time, Ord_ID, Nom_ID, Operation_ID, Map_ID) VALUES (@countC, @Ord_ID_WLTwo, '16', '3', '3')";
                cmdAddinWLTwo.Parameters.AddWithValue("@countC", countC);
                cmdAddinWLTwo.Parameters.AddWithValue("@Ord_ID_WLTwo", Ord_ID_WL);
                cmdAddinWLTwo.ExecuteScalar();

                int countD = (Count_UL * 40);
                MySqlCommand cmdAddinWLThree = new MySqlCommand();
                cmdAddinWLThree.Connection = ConnectionWithDB.GetConnection();
                cmdAddinWLThree.CommandText = "INSERT INTO work_log (Log_Work_Time, Ord_ID, Nom_ID, Operation_ID, Map_ID) VALUES (@countD, @Ord_ID_WLThree, '17', '4', '4')";
                cmdAddinWLThree.Parameters.AddWithValue("@countD", countD);
                cmdAddinWLThree.Parameters.AddWithValue("@Ord_ID_WLThree", Ord_ID_WL);
                cmdAddinWLThree.ExecuteScalar();

                int countE = (Count_UL * 60);
                MySqlCommand cmdAddinWLFour = new MySqlCommand();
                cmdAddinWLFour.Connection = ConnectionWithDB.GetConnection();
                cmdAddinWLFour.CommandText = "INSERT INTO work_log (Log_Work_Time, Ord_ID, Nom_ID, Operation_ID, Map_ID) VALUES (@countE, @Ord_ID_WLThree, '18', '0', '0')";
                cmdAddinWLFour.Parameters.AddWithValue("@countE", countE);
                cmdAddinWLFour.Parameters.AddWithValue("@Ord_ID_WLThree", Ord_ID_WL);
                cmdAddinWLFour.ExecuteScalar();

            }
            else
            {
                MySqlCommand cmdAddinWLFour = new MySqlCommand();
                cmdAddinWLFour.Connection = ConnectionWithDB.GetConnection();
                cmdAddinWLFour.CommandText = "INSERT INTO work_log (Log_Work_Time, Ord_ID, Nom_ID, Operation_ID, Map_ID) VALUES (@Log_Work_Time_WL, @Ord_ID_WLSix, @Nom_ID_WL, @Operation_ID_WL, @Map_ID_WL)";
                cmdAddinWLFour.Parameters.AddWithValue("@Log_Work_Time_WL", Log_Work_Time_WL);
                cmdAddinWLFour.Parameters.AddWithValue("@Ord_ID_WLSix", Ord_ID_WL);
                cmdAddinWLFour.Parameters.AddWithValue("@Nom_ID_WL", Nom_ID_WL);
                cmdAddinWLFour.Parameters.AddWithValue("@Operation_ID_WL", Operation_ID_WL);
                cmdAddinWLFour.Parameters.AddWithValue("@Map_ID_WL", Map_ID_WL);
                cmdAddinWLFour.ExecuteScalar();
            }

            switch(Nom_ID_WL)
            {
                case 14:
                    MySqlCommand cmdAddinULfourteenA = new MySqlCommand();
                    cmdAddinULfourteenA.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULfourteenA.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULfourteenA, @Ord_ID_WLFivefourteenA, '6')";
                    cmdAddinULfourteenA.Parameters.AddWithValue("@Count_ULfourteenA", Count_UL);
                    cmdAddinULfourteenA.Parameters.AddWithValue("@Ord_ID_WLFivefourteenA", Ord_ID_WL);
                    cmdAddinULfourteenA.ExecuteScalar();

                    MySqlCommand cmdAddinULfourteenB = new MySqlCommand();
                    cmdAddinULfourteenB.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULfourteenB.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULfourteenB, @Ord_ID_WLFivefourteenB, '7')";
                    cmdAddinULfourteenB.Parameters.AddWithValue("@Count_ULfourteenB", Count_UL);
                    cmdAddinULfourteenB.Parameters.AddWithValue("@Ord_ID_WLFivefourteenB", Ord_ID_WL);
                    cmdAddinULfourteenB.ExecuteScalar();

                    MySqlCommand cmdAddinULfourteenC = new MySqlCommand();
                    cmdAddinULfourteenC.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULfourteenC.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULfourteenC, @Ord_ID_WLFivefourteenC, '8')";
                    cmdAddinULfourteenC.Parameters.AddWithValue("@Count_ULfourteenC", Count_UL);
                    cmdAddinULfourteenC.Parameters.AddWithValue("@Ord_ID_WLFivefourteenC", Ord_ID_WL);
                    cmdAddinULfourteenC.ExecuteScalar();

                    MySqlCommand cmdAddinULfourteenD = new MySqlCommand();
                    cmdAddinULfourteenD.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULfourteenD.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULfourteenD, @Ord_ID_WLFivefourteenD, '9')";
                    cmdAddinULfourteenD.Parameters.AddWithValue("@Count_ULfourteenD", Count_UL);
                    cmdAddinULfourteenD.Parameters.AddWithValue("@Ord_ID_WLFivefourteenD", Ord_ID_WL);
                    cmdAddinULfourteenD.ExecuteScalar();

                    MySqlCommand cmdAddinULfourteenE = new MySqlCommand();
                    cmdAddinULfourteenE.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULfourteenE.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULfourteenE, @Ord_ID_WLFivefourteenE, '14')";
                    cmdAddinULfourteenE.Parameters.AddWithValue("@Count_ULfourteenE", Count_UL);
                    cmdAddinULfourteenE.Parameters.AddWithValue("@Ord_ID_WLFivefourteenE", Ord_ID_WL);
                    cmdAddinULfourteenE.ExecuteScalar();
                    break;
                case 15:
                    MySqlCommand cmdAddinULfifteenA = new MySqlCommand();
                    cmdAddinULfifteenA.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULfifteenA.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULfifteenA, @Ord_ID_WLFivefifteenA, '2')";
                    cmdAddinULfifteenA.Parameters.AddWithValue("@Count_ULfifteenA", Count_UL);
                    cmdAddinULfifteenA.Parameters.AddWithValue("@Ord_ID_WLFivefifteenA", Ord_ID_WL);
                    cmdAddinULfifteenA.ExecuteScalar();

                    MySqlCommand cmdAddinULfifteenB = new MySqlCommand();
                    cmdAddinULfifteenB.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULfifteenB.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULfifteenB, @Ord_ID_WLFivefifteenB, '3')";
                    cmdAddinULfifteenB.Parameters.AddWithValue("@Count_ULfifteenB", Count_UL);
                    cmdAddinULfifteenB.Parameters.AddWithValue("@Ord_ID_WLFivefifteenB", Ord_ID_WL);
                    cmdAddinULfifteenB.ExecuteScalar();

                    MySqlCommand cmdAddinULfifteenC = new MySqlCommand();
                    cmdAddinULfifteenC.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULfifteenC.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULfifteenC, @Ord_ID_WLFivefifteenC, '4')";
                    cmdAddinULfifteenC.Parameters.AddWithValue("@Count_ULfifteenC", Count_UL);
                    cmdAddinULfifteenC.Parameters.AddWithValue("@Ord_ID_WLFivefifteenC", Ord_ID_WL);
                    cmdAddinULfifteenC.ExecuteScalar();

                    MySqlCommand cmdAddinULfifteenD = new MySqlCommand();
                    cmdAddinULfifteenD.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULfifteenD.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULfifteenD, @Ord_ID_WLFivefifteenD, '5')";
                    cmdAddinULfifteenD.Parameters.AddWithValue("@Count_ULfifteenD", Count_UL);
                    cmdAddinULfifteenD.Parameters.AddWithValue("@Ord_ID_WLFivefifteenD", Ord_ID_WL);
                    cmdAddinULfifteenD.ExecuteScalar();

                    MySqlCommand cmdAddinULfifteenE = new MySqlCommand();
                    cmdAddinULfifteenE.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULfifteenE.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULfifteenE, @Ord_ID_WLFivefifteenE, '15')";
                    cmdAddinULfifteenE.Parameters.AddWithValue("@Count_ULfifteenE", Count_UL);
                    cmdAddinULfifteenE.Parameters.AddWithValue("@Ord_ID_WLFivefifteenE", Ord_ID_WL);
                    cmdAddinULfifteenE.ExecuteScalar();
                    break;

                case 16:
                    MySqlCommand cmdAddinULsixteenA = new MySqlCommand();
                    cmdAddinULsixteenA.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULsixteenA.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULsixteenA, @Ord_ID_WLFivesixteenA, '0')";
                    cmdAddinULsixteenA.Parameters.AddWithValue("@Count_ULsixteenA", Count_UL);
                    cmdAddinULsixteenA.Parameters.AddWithValue("@Ord_ID_WLFivesixteenA", Ord_ID_WL);
                    cmdAddinULsixteenA.ExecuteScalar();

                    MySqlCommand cmdAddinULsixteenB = new MySqlCommand();
                    cmdAddinULsixteenB.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULsixteenB.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULsixteenB, @Ord_ID_WLFivesixteenB, '1')";
                    cmdAddinULsixteenB.Parameters.AddWithValue("@Count_ULsixteenB", Count_UL);
                    cmdAddinULsixteenB.Parameters.AddWithValue("@Ord_ID_WLFivesixteenB", Ord_ID_WL);
                    cmdAddinULsixteenB.ExecuteScalar();

                    MySqlCommand cmdAddinULsixteenE = new MySqlCommand();
                    cmdAddinULsixteenE.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULsixteenE.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULsixteenE, @Ord_ID_WLFivesixteenE, '16')";
                    cmdAddinULsixteenE.Parameters.AddWithValue("@Count_ULsixteenE", Count_UL);
                    cmdAddinULsixteenE.Parameters.AddWithValue("@Ord_ID_WLFivesixteenE", Ord_ID_WL);
                    cmdAddinULsixteenE.ExecuteScalar();
                    break;

                case 17:
                    MySqlCommand cmdAddinULseventeenA = new MySqlCommand();
                    cmdAddinULseventeenA.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULseventeenA.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULseventeenA, @Ord_ID_WLFiveseventeenA, '10')";
                    cmdAddinULseventeenA.Parameters.AddWithValue("@Count_ULseventeenA", Count_UL);
                    cmdAddinULseventeenA.Parameters.AddWithValue("@Ord_ID_WLFiveseventeenA", Ord_ID_WL);
                    cmdAddinULseventeenA.ExecuteScalar();

                    MySqlCommand cmdAddinULseventeenB = new MySqlCommand();
                    cmdAddinULseventeenB.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULseventeenB.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULseventeenB, @Ord_ID_WLFiveseventeenB, '11')";
                    cmdAddinULseventeenB.Parameters.AddWithValue("@Count_ULseventeenB", Count_UL);
                    cmdAddinULseventeenB.Parameters.AddWithValue("@Ord_ID_WLFiveseventeenB", Ord_ID_WL);
                    cmdAddinULseventeenB.ExecuteScalar();

                    MySqlCommand cmdAddinULseventeenC = new MySqlCommand();
                    cmdAddinULseventeenC.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULseventeenC.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULseventeenC, @Ord_ID_WLFiveseventeenC, '12')";
                    cmdAddinULseventeenC.Parameters.AddWithValue("@Count_ULseventeenC", Count_UL);
                    cmdAddinULseventeenC.Parameters.AddWithValue("@Ord_ID_WLFiveseventeenC", Ord_ID_WL);
                    cmdAddinULseventeenC.ExecuteScalar();

                    MySqlCommand cmdAddinULseventeenD = new MySqlCommand();
                    cmdAddinULseventeenD.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULseventeenD.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULseventeenD, @Ord_ID_WLFiveseventeenD, '13')";
                    cmdAddinULseventeenD.Parameters.AddWithValue("@Count_ULseventeenD", Count_UL);
                    cmdAddinULseventeenD.Parameters.AddWithValue("@Ord_ID_WLFiveseventeenD", Ord_ID_WL);
                    cmdAddinULseventeenD.ExecuteScalar();

                    MySqlCommand cmdAddinULseventeenE = new MySqlCommand();
                    cmdAddinULseventeenE.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULseventeenE.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULseventeenE, @Ord_ID_WLFiveseventeenE, '17')";
                    cmdAddinULseventeenE.Parameters.AddWithValue("@Count_ULseventeenE", Count_UL);
                    cmdAddinULseventeenE.Parameters.AddWithValue("@Ord_ID_WLFiveseventeenE", Ord_ID_WL);
                    cmdAddinULseventeenE.ExecuteScalar();
                    break;

                case 18:
                    MySqlCommand cmdAddinULeighteenA = new MySqlCommand();
                    cmdAddinULeighteenA.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULeighteenA.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULeighteenA, @Ord_ID_WLFiveeighteenA, '14')";
                    cmdAddinULeighteenA.Parameters.AddWithValue("@Count_ULeighteenA", Count_UL);
                    cmdAddinULeighteenA.Parameters.AddWithValue("@Ord_ID_WLFiveeighteenA", Ord_ID_WL);
                    cmdAddinULeighteenA.ExecuteScalar();

                    MySqlCommand cmdAddinULeighteenB = new MySqlCommand();
                    cmdAddinULeighteenB.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULeighteenB.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULeighteenB, @Ord_ID_WLFiveeighteenB, '15')";
                    cmdAddinULeighteenB.Parameters.AddWithValue("@Count_ULeighteenB", Count_UL);
                    cmdAddinULeighteenB.Parameters.AddWithValue("@Ord_ID_WLFiveeighteenB", Ord_ID_WL);
                    cmdAddinULeighteenB.ExecuteScalar();

                    MySqlCommand cmdAddinULeighteenC = new MySqlCommand();
                    cmdAddinULeighteenC.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULeighteenC.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULeighteenC, @Ord_ID_WLFiveeighteenC, '16')";
                    cmdAddinULeighteenC.Parameters.AddWithValue("@Count_ULeighteenC", Count_UL);
                    cmdAddinULeighteenC.Parameters.AddWithValue("@Ord_ID_WLFiveeighteenC", Ord_ID_WL);
                    cmdAddinULeighteenC.ExecuteScalar();

                    MySqlCommand cmdAddinULeighteenD = new MySqlCommand();
                    cmdAddinULeighteenD.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULeighteenD.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULeighteenD, @Ord_ID_WLFiveeighteenD, '11')";
                    cmdAddinULeighteenD.Parameters.AddWithValue("@Count_ULeighteenD", Count_UL);
                    cmdAddinULeighteenD.Parameters.AddWithValue("@Ord_ID_WLFiveeighteenD", Ord_ID_WL);
                    cmdAddinULeighteenD.ExecuteScalar();

                    MySqlCommand cmdAddinULeighteenE = new MySqlCommand();
                    cmdAddinULeighteenE.Connection = ConnectionWithDB.GetConnection();
                    cmdAddinULeighteenE.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_ULeighteenE, @Ord_ID_WLFiveeighteenE, '18')";
                    cmdAddinULeighteenE.Parameters.AddWithValue("@Count_ULeighteenE", Count_UL);
                    cmdAddinULeighteenE.Parameters.AddWithValue("@Ord_ID_WLFiveeighteenE", Ord_ID_WL);
                    cmdAddinULeighteenE.ExecuteScalar();
                    break;

            }
            MySqlCommand cmdAddinUL = new MySqlCommand();
            cmdAddinUL.Connection = ConnectionWithDB.GetConnection();
            cmdAddinUL.CommandText = "INSERT INTO usage_log (Log_Count, Ord_ID, Nom_ID) VALUES (@Count_UL, @Ord_ID_WLFive, @Nom_ID_WL)";
            cmdAddinUL.Parameters.AddWithValue("@Count_UL", Count_UL);
            cmdAddinUL.Parameters.AddWithValue("@Ord_ID_WLFive", Ord_ID_WL);
            cmdAddinUL.Parameters.AddWithValue("@Nom_ID_WL", Nom_ID_WL);
            cmdAddinUL.ExecuteScalar();

            ConnectionWithDB.CloseConnection();
                
           // }
            //catch
            //{
              //  MessageBox.Show("Нет соединения с базой данных:(");
            //}

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
    }
}
