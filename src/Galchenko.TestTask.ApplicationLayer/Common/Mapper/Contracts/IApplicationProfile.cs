using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Galchenko.TestTask.ApplicationLayer.Common.Mapper.Contracts
{
    public interface IApplicationProfile
    {
        public Profile Instance { get; }

        public string PassPhrase { get; }

        public ILogger< IApplicationProfile > Logger { get; }
    }
}
