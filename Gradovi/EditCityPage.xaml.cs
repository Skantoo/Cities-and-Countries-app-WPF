using Gradovi.Dal;
using Gradovi.Models;
using Gradovi.Utils;
using Gradovi.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Gradovi
{
    /// <summary>
    /// Interaction logic for EditCityPage.xaml
    /// </summary>
    public partial class EditCityPage : FramedPage
    {
        private const string Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png";
        private readonly City city;
        
        
        public EditCityPage(CityViewModel cityViewModel, City city = null)  : base(cityViewModel)
        {
            InitializeComponent();
            
            this.city = city ?? new City();
            DataContext = city;
            
        }


        private bool FormValid()
        {
            bool valid = true;
            if (tBCityName == null)
            {
                lbError1.Content = "Add a name!";
                valid = false;
            }
            if (cbCountry == null)
            {
                lbError2.Content = "Add a country!";

                valid = false;
            }
            if (Picture.Source == null)
            {
                PictureBroder.BorderBrush = Brushes.Red;
                valid = false;
            }
            return valid;
        }

        private void btImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = Filter
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Picture.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void btCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                city.ImeGrada = tBCityName.Text.Trim();
                Country selected = cbCountry.SelectedItem as Country;
                city.CountryID = selected.IDCountry;
                city.Picture = ImageUtils.BitmapImageToByteArray(Picture.Source as BitmapImage);
                if (city.IDCity==0)
                {
                    CityViewModel.Cities.Add(city);
                }
                else
                {
                    CityViewModel.Update(city);
                }
                Frame.NavigationService.GoBack();
            }
        }

        private void btBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();

        private void cbCountry_Initialized(object sender, EventArgs e)
        {
            
            cbCountry.ItemsSource = RepositoryFactory.GetRepository().GetCountries();
        }
    }
}
