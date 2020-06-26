using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Galchenko.TestTask.ViewModels
{
    public static class ViewModelsDependencyInjection
    {
        public static void AddViewModels( this IServiceCollection services )
        {
            services.AddTransient< MainViewModel >();
        }
    }
}
