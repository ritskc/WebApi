using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bsm.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Bsm.WebApi.IServices;
using Microsoft.Extensions.Logging;
using static Bsm.WebApi.Constants.BusinessConstants;

namespace Bsm.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("Hierarchy/User")]
    //[Authorize(ActiveAuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HierarchyUserController : Controller
    {
        private readonly ILogger<HierarchyUserController> _logger;
        private readonly IHierarchyUserService _hierarchyUserService;
        private readonly IDbLoggerService _dbLoggerService;
        public HierarchyUserController(ILogger<HierarchyUserController> logger, IHierarchyUserService hierarchyUserService, IDbLoggerService dbLoggerService)
        {
            _logger = logger;
            _hierarchyUserService = hierarchyUserService;
             _dbLoggerService = dbLoggerService;
    }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await Task.Delay(0);
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            await Task.Delay(0);
            return NotFound();
        }

        [HttpPut]//replaces/updates entire hierarchy
        public async Task<IActionResult> Put([FromBody] List<HierarchyUser> hierarchies)
        {
            await Task.Delay(0);
            return NotFound();
        }

        [HttpPost]//creates new hierarchy
        public async Task Post([FromBody] List<HierarchyUser> hierarchyUsers)
        {
            var requestId = DateTime.UtcNow.ToFileTime().ToString();
            var genericSchedulesLog = new GenericSchedulesLog();
            genericSchedulesLog.GenericScheduleDateTime = DateTime.UtcNow;
            genericSchedulesLog.GenericScheduleId = 1;
            genericSchedulesLog.OrganizationId = hierarchyUsers.FirstOrDefault().OrganizationId;

            genericSchedulesLog.Log = "Info : Create Organization Hierarchy User Started.";
            _dbLoggerService.LogInfo(genericSchedulesLog);


            _logger.LogInformation("Request:{0} HttpReqeustType:{1}", requestId, HttpRequestType.GET);
            try
            {
                //await Task.Delay(0hierarchyUsers
                //await _hierarchyUserService.SaveOrganizationHierarchyUserAssignment(hierarchyUser, genericSchedulesLog);
                _logger.LogInformation("Response:{0}  is succeed", requestId);
            }
            catch (Exception ex)
            {
                await Task.Delay(0);
                _logger.LogError("Response:{0}  is failed Error : {1}", requestId, ex);
                NotFound();
            }
        }

        [HttpDelete]//deletes entire hierarchy for given organization
        public async Task<IActionResult> Delete(int Id)
        {
            await Task.Delay(0);
            return NotFound();
        }
    }
}