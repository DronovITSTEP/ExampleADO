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
        UpdateQuery UpdateQuery;
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
            //SelectQuery = new SelectQuery(dbConnection);
            UpdateQuery = new UpdateQuery(dbConnection, factory);
        }

        async private void Sart_Click(object sender, RoutedEventArgs e)
        {           
            dataGrid.ItemsSource = await SelectQuery.SelectAll();
        }

        private async void second_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = await SelectQuery.SelectCountry("Россия");
        }

        private void third_Click(object sender, RoutedEventArgs e)
        {
            //UpdateQuery.Insert(new Country { Name = "Псков", PartWorld = 1});
        }

        private void fourth_Click(object sender, RoutedEventArgs e)
        {
            UpdateQuery.Update();
        }

        private void fiveth_Click(object sender, RoutedEventArgs e)
        {
            //UpdateQuery.Delete(new City { Name = "Псков"});
        }
    }
}
