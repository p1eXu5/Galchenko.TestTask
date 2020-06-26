using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Patients.Dtos;
using Galchenko.TestTask.Domain;
using Galchenko.TestTask.Domain.Enums;
using p1eXu5.Wpf.MvvmBaseLibrary;
using DialogViewModel = p1eXu5.Wpf.MvvmLibrary.DialogViewModel;

namespace Galchenko.TestTask.ViewModels
{
    public class PatientUpdateViewModel : PatientViewModelBase< PatientUpdateDto >
    {
        public PatientUpdateViewModel( PatientUpdateDto patient, 
                                       IValidator< PatientUpdateDto > patientValidator, 
                                       DialogRepository dialogRepository ) 
            : base( patient, patientValidator, dialogRepository )
        {
            Address = new AddressViewModel( patient.Address );
        }


        public AddressViewModel Address { get; }
    }
}
