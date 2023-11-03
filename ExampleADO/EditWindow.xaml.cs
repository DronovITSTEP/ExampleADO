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
        CountryQuery countryQuery;
        CityQuery cityQuery;

        int initMethod;
        public EditWindow(IConnection con, int i)
        {
            InitializeComponent();

            this.initMethod = i;
            if (con is CityQuery)
                cityQuery = (CityQuery)con;
            else if (con is CountryQuery)
                countryQuery = (CountryQuery)con;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (countryQuery != null)
                CallMethod(countryQuery, InitCountry());
            else if (cityQuery != null)
                CallMethod(cityQuery, InitCity());
        }
        private City InitCity() {
            City city = new City();
            city.Name = Name.Text;
            city.Population = int.Parse(Number.Text);
            return city;
        }
        private Country InitCountry() {
            Country country = new Country();
            country.Name = Name.Text;
            country.PartOfTheWorldId = int.Parse(Number.Text);
            return country;
        }
        private void CallMethod<T>(IQuery<T> query, T c)
        {
            if (initMethod == 1)
            {
                query.Insert(c);
            }
            else if (initMethod == 2)
            {
                query.Update(c);
            }
            else if (initMethod == 3)
            {
                query.Delete(c);
            }
        }
    }
}