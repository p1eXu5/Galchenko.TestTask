using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p1eXu5.Wpf.MvvmLibrary;

namespace Galchenko.TestTask.ViewModels
{
    public class ErrorViewModel : DialogViewModel
    {
        private string _errorMessage;

        public ErrorViewModel( string errorMessage = "" )
        {
            _errorMessage = errorMessage;
        }


        public string Message
        {
            get => _errorMessage;
            set {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
    }
}
