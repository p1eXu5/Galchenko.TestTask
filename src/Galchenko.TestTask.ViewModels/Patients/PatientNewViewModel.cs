using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Galchenko.TestTask.ApplicationLayer.Patients.Dtos;
using p1eXu5.Wpf.MvvmBaseLibrary;

namespace Galchenko.TestTask.ViewModels.Patients
{
    public class PatientNewViewModel : PatientViewModelBase< PatientNewDto >
    {
        public PatientNewViewModel( PatientNewDto patient, 
                                    IValidator< PatientNewDto > patientValidator, 
                                    DialogRepository dialogRepository ) 
            : base( patient, patientValidator, dialogRepository )
        {
            Address = new AddressViewModel( patient.Address );
        }

        public AddressViewModel Address { get; }
    }
}
