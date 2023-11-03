using ExampleADO.DBWork;
using ExampleADO.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExampleADO
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    enum CHOICE { ADD = 1, UPDATE, DELETE }
    public partial class MainWindow : Window
    {
        DbConnection dbConnection;
        DbProviderFactory factory;
        public MainWindow()
        {
            InitializeComponent();
            factory = DbProviderFactories
                .GetFactory(ConfigurationManager
                        .ConnectionStrings["MyCountries"]
                        .ProviderName);
            dbConnection = factory.CreateConnection();
            dbConnection.ConnectionString = ConfigurationManager
                        .ConnectionStrings["MyCountries"]
                        .ConnectionString;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IConnection con;

            if (radioCity.IsChecked == true)
                con = new CityQuery(dbConnection, factory);
            else
                con = new CountryQuery(dbConnection, factory);

            EditWindow editWindow = null;

            if ((sender as Button) == InsertButton)
                editWindow = new EditWindow(con, (int)CHOICE.ADD);
            else if ((sender as Button) == UpdateButton)
                editWindow = new EditWindow(con, (int)CHOICE.UPDATE);
            else if ((sender as Button) == DeleteButton)
                editWindow = new EditWindow(con, (int)CHOICE.DELETE);

            editWindow.Show();
        }
    }
}
