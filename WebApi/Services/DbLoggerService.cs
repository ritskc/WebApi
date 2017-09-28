using Bsm.WebApi.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bsm.WebApi.Models;
using Bsm.WebApi.Repositories;
using Bsm.WebApi.IRepositories;

namespace Bsm.WebApi.Services
{
    public class DbLoggerService : IDbLoggerService
    {

        private readonly IDbLoggerRepository _dbLoggerRepository;       

        public DbLoggerService(IDbLoggerRepository dbLoggerRepository)
        {
            _dbLoggerRepository = dbLoggerRepository;
        }
        public void LogInfo(GenericSchedulesLog genericSchedulesLog)
        {
            _dbLoggerRepository.LogInfo(genericSchedulesLog);
        }
    }
}
