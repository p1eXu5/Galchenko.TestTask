using System;
using Galchenko.TestTask.ApplicationLayer.Patients.Dtos;
using Galchenko.TestTask.ViewModels.Contracts;
using p1eXu5.Wpf.MvvmLibrary;

namespace Galchenko.TestTask.ViewModels.Patients
{
    public class PatientRowViewModel : ViewModelBase, IIdViewModel< string >
    {

        public PatientRowViewModel( PatientUpdateDto patient )
        {
            Patient = patient;
            AddressVm = new AddressViewModel( Patient.Address );
        }

        public PatientUpdateDto Patient { get; }


        public string Id => Patient.Id;
        public string Name => $"{Patient.LastName} {Patient.FirstName} {Patient.MiddleName ?? ""}";
        public DateTime DateOfBirth => Patient.DateOfBirth;
        public string Phone => Patient.Phone;
        public AddressViewModel AddressVm { get; }
    }
}
