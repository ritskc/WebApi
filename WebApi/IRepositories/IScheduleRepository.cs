using Bsm.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsm.WebApi.IRepositories
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Schedule>> GetAllScheduleTask();
        IEnumerable<Schedule> GetAllScheduleTasks();
        void UpdateScheduleTask(int scheduleId);
    }
}
