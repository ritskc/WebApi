using Bsm.WebApi.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bsm.WebApi.Models;
using Bsm.WebApi.IRepositories;

namespace Bsm.WebApi.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;       

        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }
        public Task<IEnumerable<Schedule>> GetAllScheduleTask()
        {
            return _scheduleRepository.GetAllScheduleTask();
        }

        public async Task<IEnumerable<Schedule>> GetAllScheduleTaskByOrganization(int OrganizationId)
        {
            var schedules = await  _scheduleRepository.GetAllScheduleTask();            
            return schedules.Where(e => e.OrganizationID == OrganizationId);
        }

        public void UpdateScheduleTask(int scheduleId)
        {
            _scheduleRepository.UpdateScheduleTask(scheduleId);
        }
    }
}
