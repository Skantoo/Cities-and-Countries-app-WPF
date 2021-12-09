using Gradovi.Models;
using Gradovi.Utils;
using Gradovi.ViewModels;
using Microsoft.Win32;
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
    /// Interaction logic for EditCountryPage.xaml
    /// </summary>
    public partial class EditCountryPage : FramedPage
    {
        private const string Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png";

        private readonly Country country;
        public EditCountryPage(CountryViewModel countryViewModel, Country country=null): base(countryViewModel)
        {
            InitializeComponent();
            this.country = country ?? new Country();
            DataContext = country;
        }

        private bool FormValid()
        {
            bool valid = true;
            if (tbCountryName == null)
            {
                lbError1.Content = "Add a name!";
                valid = false;
            }
            
            if (Picture.Source == null)
            {
                PictureBorder.BorderBrush = Brushes.Red;
                valid = false;
            }
            return valid;
        }
        private void btCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                country.ImeDrzave = tbCountryName.Text.Trim();
                country.Picture = ImageUtils.BitmapImageToByteArray(Picture.Source as BitmapImage);
                if (country.IDCountry==0)
                {
                    CountryViewModel.Countries.Add(country);
                }
                else
                {
                    CountryViewModel.Update(country);
                }
                Frame.NavigationService.GoBack();
            }
        }

        private void btImage_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmapImage = new BitmapImage();
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = Filter
            };
            if (openFileDialog.ShowDialog() == true)
            {
                bitmapImage = new BitmapImage(new Uri(openFileDialog.FileName));
                Picture.Source = bitmapImage;
            }
        }

        private void btBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();
    }
}
