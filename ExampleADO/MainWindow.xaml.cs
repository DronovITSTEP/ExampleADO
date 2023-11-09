using ExampleADO.DBWork;
using System.Configuration;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;


namespace ExampleADO
{
    public partial class MainWindow : Window
    {
        private DbConnection dbConnection;
        private DbProviderFactory factory;
        private AbstractQuery abstractQuery;
        SelectQuery selectQuery;
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

            selectQuery = new SelectQuery(dbConnection);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
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

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        { 
            dataGrid.ItemsSource = await selectQuery.SelectAll();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = await selectQuery.SelectCountry(CountryName.Text);
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = await selectQuery.SelectCityInCountry(CityNameByCountry.Text);
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = await selectQuery.SelectCountryStartChar(CountryChar.Text + "%");
        }

        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = await selectQuery.SelectCapitalStartChar(CapitalChar.Text+"%");
        }

        private async void Button_Click_6(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = await selectQuery.SelectTop3CapitalMinPopulation();
        }

        private async void Button_Click_7(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = await selectQuery.SelectTop3CountryMinPopulation();
        }

        private async void Button_Click_8(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = await selectQuery.SelectAvgPopulationInCapital();
        }

        private async void Button_Click_9(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = await selectQuery.SelectTop3PartWorldMinPopulation();
        }

        private async void Button_Click_10(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = await selectQuery.SelectTop3PartWorldMaxPopulation();
        }

        private async void Button_Click_11(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = await selectQuery.SelectAvgPopulationInCountry(CountryName2.Text);
        }

        private async void Button_Click_12(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = await selectQuery.SelectCityWhithMinPopulationInCountry(CountryName3.Text);
        }

        private async void Button_Click_13(object sender, RoutedEventArgs e)
        {
            string query = string.Empty;
            if (CheckCoutryName.IsChecked == true) query += "Countries.Name Страна, ";
            if (CheckCapital.IsChecked == true) query += "Cities.Name Город, ";
            if (CheckPopulation.IsChecked == true) query += "sum(Cities.Population), ";
            if (CheckPartOfTheWorld.IsChecked == true) query += "PartOfTheWorld.Name [Часть света], ";
            if (query != string.Empty)
            {
                query = query.Substring(0, query.LastIndexOf(","));
                dataGrid.ItemsSource = await selectQuery.SelectAllWithParameters(query);
            }
        }
    }
}
