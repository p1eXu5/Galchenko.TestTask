using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p1eXu5.Wpf.MvvmLibrary;

namespace Galchenko.TestTask.ViewModels
{
    public class DateViewModel : ViewModelBase
    {

        public DateViewModel( DateTime date )
        {
            Date = date;
            MaxYear = DateTime.Now.Year;
            MinYear = 1900;
        }

        public DateTime Date { get; private set; }

        public int Day
        {
            get => Date.Day;
            set {
                Date = 
                    new DateTime(
                        Date.Year,
                        Date.Month,
                        value );

                OnPropertyChanged();
            }
        }

        public int Month
        {
            get => Date.Month;
            set {
                try {
                    Date = 
                        new DateTime(
                            Date.Year,
                            value,
                            Date.Day );
                }
                catch ( ArgumentOutOfRangeException ) {
                    Date = 
                        new DateTime(
                            Date.Year,
                            value,
                            1 );
                    OnPropertyChanged(nameof(Day));
                }

                OnPropertyChanged();
            }
        }

        public int Year
        {
            get => Date.Year;
            set {
                try {
                    Date = 
                        new DateTime(
                            value,
                            Date.Month,
                            Date.Day );

                }
                catch ( ArgumentOutOfRangeException ) {
                    Date = 
                        new DateTime(
                            value,
                            Date.Month,
                            1 );
                    OnPropertyChanged(nameof(Day));
                }

                OnPropertyChanged();
            }
        }

        public int MinYear { get; }
        public int MaxYear { get; }
    }
}
