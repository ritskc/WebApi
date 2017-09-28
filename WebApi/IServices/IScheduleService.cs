using Bsm.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsm.WebApi.IServices
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> GetAllScheduleTask();
        Task<IEnumerable<Schedule>> GetAllScheduleTaskByOrganization(int OrganizationId);
        void UpdateScheduleTask(int scheduleId);
    }
}
