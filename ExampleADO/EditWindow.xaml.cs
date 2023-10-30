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
        private int id;
        CountryQuery countryQuery;
        public EditWindow(DbConnection dbConnection, DbProviderFactory factory, int id)
        {
            InitializeComponent();
            countryQuery = new CountryQuery(dbConnection, factory);
            this.id = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Country country = new Country { Name = NameCountry.Text, 
                PartOfTheWorldId = PartWorlds.SelectedIndex+1, Id = int.Parse(IdCountry.Text)};
            if (id == 1)
                countryQuery.Insert(country);
            else if (id == 2)
                countryQuery.Update(country);
            else if (id == 3)
                countryQuery.Delete(country);
        }
    }
}
