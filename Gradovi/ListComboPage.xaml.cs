using Gradovi.ViewModels;
using Gradovi.Models;
using Gradovi;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Gradovi
{
    /// <summary>
    /// Interaction logic for ListComboPage.xaml
    /// </summary>
    public partial class ListComboPage : FramedPage
    {
        public ListComboPage(CountryViewModel countryViewModel,CityViewModel cityViewModel) : base(countryViewModel,cityViewModel)
        {
            InitializeComponent();
            lvCountry.ItemsSource = countryViewModel.Countries;
            lvCity.ItemsSource = cityViewModel.Cities;
        }

        private void BtnAddCountry_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new EditCountryPage(CountryViewModel) { Frame = Frame });

        private void BtnAddCity_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new EditCityPage(CityViewModel) { Frame = Frame });

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvCity.SelectedItem != null)
            {
                CityViewModel.Cities.Remove(lvCity.SelectedItem as City);
            }
            else if (lvCountry.SelectedItem != null)
            {
                CountryViewModel.Countries.Remove(lvCountry.SelectedItem as Country);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvCity.SelectedItem != null)
            {
                Frame.Navigate(new EditCityPage(CityViewModel, lvCity.SelectedItem as City) { Frame = Frame });
            }
            if (lvCountry.SelectedItem != null)
            {
                Frame.Navigate(new EditCountryPage(CountryViewModel, lvCountry.SelectedItem as Country) { Frame = Frame });
            }
        }
    }
}
