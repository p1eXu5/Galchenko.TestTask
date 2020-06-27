using Galchenko.TestTask.ViewModels.Appointments;
using Galchenko.TestTask.ViewModels.Patients;
using Microsoft.Extensions.DependencyInjection;

namespace Galchenko.TestTask.ViewModels
{
    public static class ViewModelsDependencyInjection
    {
        public static void AddViewModels( this IServiceCollection services )
        {
            services.AddTransient< MainViewModel_v2 >();
            services.AddTransient< PatientCrudViewModel >();
            services.AddTransient< AppointmentCrudViewModel >();
        }
    }
}
