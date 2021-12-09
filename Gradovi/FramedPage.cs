using Gradovi.Models;
using Gradovi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Gradovi
{
    public class FramedPage : Page
    {
        public FramedPage(CountryViewModel countryViewModel, CityViewModel cityViewModel)
        {
            CountryViewModel = countryViewModel;
            CityViewModel = cityViewModel;

        }
        public FramedPage(CountryViewModel countryViewModel)
        {
            CountryViewModel = countryViewModel;
            
        }
        public FramedPage( CityViewModel cityViewModel)
        {
            
            CityViewModel = cityViewModel;
        }
        

        public CountryViewModel CountryViewModel { get; }
        public CityViewModel CityViewModel { get; }
        public Frame Frame { get; set; }
    }
}
