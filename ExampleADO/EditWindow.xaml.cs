using ExampleADO.DBWork;
using ExampleADO.Models;
using System;
using System.Windows;

namespace ExampleADO
{
    public partial class EditWindow : Window
    {
        AbstractQuery baseQuery;

        public event Action<Country> eventCountry;
        public event Action<City> eventCity;
        public EditWindow(AbstractQuery aq)
        {
            InitializeComponent();
            baseQuery = aq;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (baseQuery.GetType() == typeof(CountryQuery))
                eventCountry(new Country
                {
                    Id = int.Parse(Number.Text),
                    Name = Name.Text,
                    PartOfTheWorldId = PartWorlds.SelectedIndex+1
                });
            else if (baseQuery.GetType() == typeof(CityQuery))
                eventCity(new City
                {
                    Name = Name.Text,
                    Population = int.Parse(Number.Text)
                });
        }
    }
}