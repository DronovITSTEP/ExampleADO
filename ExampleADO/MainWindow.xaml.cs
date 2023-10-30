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
    public partial class MainWindow : Window
    {
        DbConnection dbConnection;
        DbProviderFactory factory;

        SelectQuery SelectQuery;
        CountryQuery UpdateQuery;
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

            UpdateQuery = new CountryQuery(dbConnection, factory);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow(dbConnection, factory, 1);
            editWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow(dbConnection, factory, 2);
            editWindow.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow(dbConnection, factory, 3);
            editWindow.Show();
        }
    }
}
