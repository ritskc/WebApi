using Bsm.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsm.WebApi.IRepositories
{
    public interface IDbLoggerRepository
    {
        void LogInfo(GenericSchedulesLog genericSchedulesLog);
    }
}
