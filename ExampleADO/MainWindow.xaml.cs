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
            AbstractQuery abstractQuery;

            if (radioCity.IsChecked == true)
                abstractQuery = new CityQuery(dbConnection, factory);
            else
                abstractQuery = new CountryQuery(dbConnection, factory);

            EditWindow editWindow = editWindow = new EditWindow(abstractQuery);

            if ((sender as Button) == InsertButton)
                editWindow.eventCountry += abstractQuery.Insert;

            else if ((sender as Button) == UpdateButton)
                editWindow.eventCountry += abstractQuery.Update;

            else if ((sender as Button) == DeleteButton)
                editWindow.eventCountry += abstractQuery.Delete;

            editWindow.Show();
        }
    }
}
