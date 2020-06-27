using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galchenko.TestTask.ApplicationLayer.Common;
using Galchenko.TestTask.Domain.Contracts;
using p1eXu5.Wpf.MvvmBaseLibrary;

namespace Galchenko.TestTask.ViewModels.Contracts
{
    public interface INewDtoViewModel< out TNewDto > : IDialogCloseRequested
        where TNewDto : notnull, IEntityDto
    {
        TNewDto Dto { get; }
    }
}
