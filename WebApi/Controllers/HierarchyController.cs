using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bsm.WebApi.Models;
using Bsm.WebApi.IServices;
using Microsoft.Extensions.Logging;
using static Bsm.WebApi.Constants.BusinessConstants;

namespace Bsm.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("Hierarchy")]
    public class HierarchyController : Controller
    {
        //private readonly IHierarchyService _hierarchyService;
        private readonly ILogger<HierarchyController> _logger;
        private readonly IHierarchyService _hierarchyService;
        private readonly IDbLoggerService _dbLoggerService;
        public HierarchyController(ILogger<HierarchyController> logger, IHierarchyService hierarchyService, IDbLoggerService dbLoggerService)
        {
            _logger = logger;
            _hierarchyService = hierarchyService;
            _dbLoggerService = dbLoggerService;
        }

        //public HierarchyController( IHierarchyService hierarchyService)
        //{

        //    _hierarchyService = hierarchyService;
        //}
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await Task.Delay(0);
            return NotFound();
        }

        [HttpGet("{organizationId}")]
        public async Task<IActionResult> Get(int organizationId)
        {
            await Task.Delay(0);
            return NotFound();
        }

        [HttpPut]//replaces/updates entire hierarchy
        public async Task<IActionResult> Put([FromBody] List<Hierarchy> hierarchies)
        {
            await Task.Delay(0);
            return NotFound();
        }

        [HttpPost]//creates new hierarchy
        public async Task Post([FromBody]HierarchyContainer hierarchiesContainer)
        {
            _logger.LogInformation("Create Hierarchy Started");

            if (hierarchiesContainer.hierarchies != null)
                _logger.LogInformation("Total hierarchy :{0} ", hierarchiesContainer.hierarchies.Count.ToString());
            else
                _logger.LogInformation("Total hierarchy : 0 ");

            if (hierarchiesContainer.hierarchiesVehicles != null)
                _logger.LogInformation("Total vehicles :{0} ", hierarchiesContainer.hierarchiesVehicles.Count.ToString());
            else
                _logger.LogInformation("Total vehicles : 0 ");


            var requestId = DateTime.UtcNow.ToFileTime().ToString();

            var genericSchedulesLog = new GenericSchedulesLog();
            genericSchedulesLog.GenericScheduleDateTime = DateTime.UtcNow;
            genericSchedulesLog.GenericScheduleId = hierarchiesContainer.schedule.Id;

            if (hierarchiesContainer.hierarchies != null)
                genericSchedulesLog.OrganizationId = hierarchiesContainer.hierarchies.FirstOrDefault().OrganizationId;

            genericSchedulesLog.Log = "Info : Create Organization Hierarchy Started.";


            _logger.LogInformation("Request:{0} HttpReqeustType:{1}", requestId, HttpRequestType.GET);
            try
            {
                genericSchedulesLog.Log = MessageType.INFO  + " :Service Started at" + DateTime.UtcNow;
                genericSchedulesLog.LogType = 1;
                _dbLoggerService.LogInfo(genericSchedulesLog);
                genericSchedulesLog.LogType = 0;

                genericSchedulesLog.Log = MessageType.INFO + " :Total hierarchy records :"+ hierarchiesContainer.hierarchies.Count.ToString() + " :Total vehicle records :" + hierarchiesContainer.hierarchiesVehicles.Count.ToString();
                genericSchedulesLog.LogType = 1;
                _dbLoggerService.LogInfo(genericSchedulesLog);
                genericSchedulesLog.LogType = 0;


                var task = _hierarchyService.CreateHierarchy(hierarchiesContainer, genericSchedulesLog);
                _logger.LogInformation("Response:{0}  is succeed", requestId);

                if (task.IsCompleted)
                {
                    genericSchedulesLog.Log = MessageType.INFO + " :Create Organization Hierarchy Ended.";
                    _dbLoggerService.LogInfo(genericSchedulesLog);
                }
            }
            catch (Exception ex)
            {
                await Task.Delay(0);
               
                genericSchedulesLog.Log = MessageType.ERR + " :Create Organization Hierarchy Failed. " + ex.ToString();
                _dbLoggerService.LogInfo(genericSchedulesLog);
                genericSchedulesLog.LogType = 0;
                NotFound();

            }
            finally
            {
                genericSchedulesLog.Log = MessageType.INFO  +" :Service ended at" + DateTime.UtcNow;
                genericSchedulesLog.LogType = 1;
                _dbLoggerService.LogInfo(genericSchedulesLog);
                genericSchedulesLog.LogType = 0;

            }

        }



        [HttpDelete]//deletes entire hierarchy for given organization
        public async Task<IActionResult> Delete(int organizationId)
        {
            await Task.Delay(0);
            return NotFound();
        }
    }
}