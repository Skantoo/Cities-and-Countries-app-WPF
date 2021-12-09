using Gradovi.Dal;
using Gradovi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gradovi.ViewModels
{
    public class CityViewModel
    {
        public ObservableCollection<City> Cities { get; }

        public CityViewModel()
        {
            Cities = new ObservableCollection<City>(RepositoryFactory.GetRepository().GetCities());
            Cities.CollectionChanged += Cities_CollectionChanged;
        }

        private void Cities_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddCity(Cities[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeleteCity(e.OldItems.OfType<City>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdateCity(e.NewItems.OfType<City>().ToList()[0]);
                    break;
            }
        }
        internal void Update(City city) => Cities[Cities.IndexOf(city)] = city;

    }
}
