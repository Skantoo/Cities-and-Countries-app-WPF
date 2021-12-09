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
    public class CountryViewModel
    {
        public ObservableCollection<Country> Countries { get; }

        public CountryViewModel()
        {
            Countries = new ObservableCollection<Country>(RepositoryFactory.GetRepository().GetCountries());
            Countries.CollectionChanged += Countries_CollectionChanged;
        }

        private void Countries_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddCountry(Countries[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeleteCountry(e.OldItems.OfType<Country>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdateCountry(e.NewItems.OfType<Country>().ToList()[0]);
                    break;
            }
        }

        internal void Update(Country country) => Countries[Countries.IndexOf(country)] = country;
    }
}
