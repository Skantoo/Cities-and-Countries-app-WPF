using Gradovi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradovi.Dal
{
    public interface IRepository
    {
        
        void AddCountry(Country country);
        void AddCity(City city);
        IList<Country> GetCountries();
        IList<City> GetCities();
        Country GetCountry(int IDCountry);
        City GetCity(int IDCity);
        void DeleteCountry(Country country);
        void DeleteCity(City city);
        void UpdateCountry(Country country);
        void UpdateCity(City city);
        
    }
}
