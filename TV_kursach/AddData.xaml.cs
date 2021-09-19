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
    /// Логика взаимодействия для AddData.xaml
    /// </summary>
    public partial class AddData : Window
    {
        string selectedItem;
        public AddData(string value)
        {
            InitializeComponent();
            selectedItem = value;

        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            addIcon(selectedItem);
        }

        public void addIcon(string SelectedItem)
        {
            //Grid grid = new Grid();
            
            //grid.ShowGridLines = true;
            //grid.Width = 250;
            //grid.Height = 100;
            switch (SelectedItem)
            {
                case "Заказы":
                    for (int j = 0; j < 5; j++)
                    {
                        grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
                    }

                    for (int j = 0; j < 2; j++)
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });
                    }
                    int i = 1;

                    for (int j = 0; j < 12; j++)
                    {
                        TextBox tb = new TextBox
                        {
                            Name = "TxtBl" + j,
                            Width = 200
                        };
                        grid.Children.Add(tb);
                        Grid.SetRow(tb, j);
                        Grid.SetColumn(tb, i);
                    }
                    i = 0;
                    TextBlock tblSt = new TextBlock
                    {
                        Name = "tblSt",
                        Text = "Статус заказа",
                        Width = 190
                    };
                    grid.Children.Add(tblSt);
                    Grid.SetRow(tblSt, 0);
                    Grid.SetColumn(tblSt, i);
                    TextBlock tblCount = new TextBlock
                    {
                        Name = "tblCount",
                        Text = "Количество",
                        Width = 190
                    };
                    grid.Children.Add(tblCount);
                    Grid.SetRow(tblCount, 1);
                    Grid.SetColumn(tblCount, i);
                    TextBlock tblData = new TextBlock
                    {
                        Name = "tblData",
                        Text = "Дата создания заказа",
                        Width = 190
                    };
                    grid.Children.Add(tblData);
                    Grid.SetRow(tblData, 2);
                    Grid.SetColumn(tblData, i);
                    TextBlock tblPlanData = new TextBlock
                    {
                        Name = "tblData",
                        Text = "Плановая дата выполнения заказа",
                        Width = 190
                    };
                    grid.Children.Add(tblPlanData);
                    Grid.SetRow(tblPlanData, 3);
                    Grid.SetColumn(tblPlanData, i);
                    TextBlock tblNom = new TextBlock
                    {
                        Name = "tblNom",
                        Text = "ID номенклатуры",
                        Width = 190
                    };
                    grid.Children.Add(tblNom);
                    Grid.SetRow(tblNom, 4);
                    Grid.SetColumn(tblNom, i);
                    break;
                case "Номенклатура":
                    for (int j = 0; j < 3; j++)
                    {
                        grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
                    }

                    for (int j = 0; j < 2; j++)
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });
                    }
                    i = 1;

                    for (int j = 0; j < 12; j++)
                    {
                        TextBox tb = new TextBox
                        {
                            Name = "TxtBl" + j,
                            Width = 200
                        };
                        grid.Children.Add(tb);
                        Grid.SetRow(tb, j);
                        Grid.SetColumn(tb, i);
                    }
                    i = 0;
                    TextBlock tblDesc = new TextBlock
                    {
                        Name = "tblDesc",
                        Text = "Описание",
                        Width = 190
                    };
                    grid.Children.Add(tblDesc);
                    Grid.SetRow(tblDesc, 0);
                    Grid.SetColumn(tblDesc, i);
                    TextBlock tblCard = new TextBlock
                    {
                        Name = "tblCard",
                        Text = "ID Технологической карты",
                        Width = 190
                    };
                    grid.Children.Add(tblCard);
                    Grid.SetRow(tblCard, 1);
                    Grid.SetColumn(tblCard, i);
                    TextBlock tblSpec = new TextBlock
                    {
                        Name = "tblSpec",
                        Text = "ID спецификатора",
                        Width = 190
                    };
                    grid.Children.Add(tblSpec);
                    Grid.SetRow(tblSpec, 2);
                    Grid.SetColumn(tblSpec, i);
                    break;
                case "Склады":
                    for (int j = 0; j < 1; j++)
                    {
                        grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
                    }

                    for (int j = 0; j < 2; j++)
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });
                    }
                    i = 1;

                    for (int j = 0; j < 12; j++)
                    {
                        TextBox tb = new TextBox
                        {
                            Name = "TxtBl" + j,
                            Width = 200
                        };
                        grid.Children.Add(tb);
                        Grid.SetRow(tb, j);
                        Grid.SetColumn(tb, i);
                    }
                    i = 0;
                    TextBlock tblDescStore = new TextBlock
                    {
                        Name = "tblDescStore",
                        Text = "Описание",
                        Width = 190
                    };
                    grid.Children.Add(tblDescStore);
                    Grid.SetRow(tblDescStore, 0);
                    Grid.SetColumn(tblDescStore, i);
                    break;
                case "Рабочие центры":
                    for (int j = 0; j < 2; j++)
                    {
                        grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
                    }

                    for (int j = 0; j < 2; j++)
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });
                    }
                    i = 1;

                    for (int j = 0; j < 12; j++)
                    {
                        TextBox tb = new TextBox
                        {
                            Name = "TxtBl" + j,
                            Width = 200
                        };
                        grid.Children.Add(tb);
                        Grid.SetRow(tb, j);
                        Grid.SetColumn(tb, i);
                    }
                    i = 0;
                    TextBlock tblDescWC = new TextBlock
                    {
                        Name = "tblDescWC",
                        Text = "Описание",
                        Width = 190
                    };
                    grid.Children.Add(tblDescWC);
                    Grid.SetRow(tblDescWC, 0);
                    Grid.SetColumn(tblDescWC, i);
                    TextBlock tblTime = new TextBlock
                    {
                        Name = "tblTime",
                        Text = "Время работы",
                        Width = 190
                    };
                    grid.Children.Add(tblTime);
                    Grid.SetRow(tblTime, 1);
                    Grid.SetColumn(tblTime, i);
                    break;
                case "Запасы":
                    for (int j = 0; j < 3; j++)
                    {
                        grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
                    }

                    for (int j = 0; j < 2; j++)
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(250) });
                    }
                    i = 1;

                    for (int j = 0; j < 12; j++)
                    {
                        TextBox tb = new TextBox
                        {
                            Name = "TxtBl" + j,
                            Width = 200
                        };
                        grid.Children.Add(tb);
                        Grid.SetRow(tb, j);
                        Grid.SetColumn(tb, i);
                    }
                    i = 0;
                    TextBlock tblIDNom = new TextBlock
                    {
                        Name = "tblIDNom",
                        Text = "ID номенклатуры",
                        Width = 190
                    };
                    grid.Children.Add(tblIDNom);
                    Grid.SetRow(tblIDNom, 0);
                    Grid.SetColumn(tblIDNom, i);
                    TextBlock tblIDstore = new TextBlock
                    {
                        Name = "tblIDstore",
                        Text = "ID склада",
                        Width = 190
                    };
                    grid.Children.Add(tblIDstore);
                    Grid.SetRow(tblIDstore, 1);
                    Grid.SetColumn(tblIDstore, i);
                    TextBlock tblCo = new TextBlock
                    {
                        Name = "tblCo",
                        Text = "Количество",
                        Width = 190
                    };
                    grid.Children.Add(tblCo);
                    Grid.SetRow(tblCo, 2);
                    Grid.SetColumn(tblCo, i);
                    break;

            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            /*ConnectionWithDB.OpenConnection();
            MySqlCommand cmd = new MySqlCommand();
            string nameTable = "";
            switch(selectedItem)
            {
                case "Заказы":
                    cmd.CommandText = "INSERT INTO ordering VALUES (@Ord_Status, @Ord_Count, @Ord_Date, @Ord_DatePlan, @Nom_ID)";
                    cmd.Parameters.AddWithValue("@Ord_Status", TxtBl0);
                    break;
                case "Номенклатура":
                    nameTable = "nomenclature";
                    break;
                case "Склады":
                    nameTable = "storage";
                    break;
                case "Рабочие центры":
                    nameTable = "work_center";
                    break;
                case "Запасы":
                    nameTable = "stocks";
                    break;
            }

            cmd.CommandText = "INSERT INTO @Tab VALUES ()";
            cmd.Parameters.AddWithValue("@Tab", nameTable);
            cmd.Connection = ConnectionWithDB.GetConnection();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            ConnectionWithDB.CloseConnection();
        }*/
        }
    }
}
