using ExampleADO.DBWork;
using ExampleADO.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ExampleADO
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
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
                    Id = 1011,
                    Name = Name.Text,
                    PartOfTheWorldId = int.Parse(Number.Text)
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