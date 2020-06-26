using Galchenko.TestTask.ApplicationLayer.Addresses.Dtos;
using p1eXu5.Wpf.MvvmLibrary;

namespace Galchenko.TestTask.ViewModels
{
    public class AddressViewModel : ViewModelBase
    {
        private readonly AddressNewDto _address;

        public AddressViewModel( AddressNewDto address )
        {
            _address = address;
        }


        public string Line1
        {
            get => _address.Line1;
            set {
                _address.Line1 = value;
                OnPropertyChanged();
            }
        }

        public string? Line2
        {
            get => _address.Line2;
            set {
                _address.Line2 = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get => _address.City;
            set {
                _address.City = value;
                OnPropertyChanged();
            }
        }

        public string PostalCode
        {
            get => _address.PostalCode;
            set {
                _address.PostalCode = value;
                OnPropertyChanged();
            }
        }
    }
}
