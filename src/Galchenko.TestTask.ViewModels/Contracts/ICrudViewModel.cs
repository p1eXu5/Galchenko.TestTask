using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Patients.Dtos;
using p1eXu5.Wpf.MvvmLibrary;

namespace Galchenko.TestTask.ViewModels.Contracts
{
    public interface ICrudViewModel : INotifyPropertyChanged
    {
        IAsyncCommand LoadDataCommand { get; }

        IAsyncCommand CreateCommand { get; }

        IAsyncCommand UpdateCommand { get; }

        IAsyncCommand DeleteCommand { get; }
    }
}
