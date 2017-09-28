using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bsm.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Bsm.WebApi.IServices;
using static Bsm.WebApi.Constants.BusinessConstants;

namespace Bsm.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("Hierarchy/Vehicle")]
    //[Authorize(ActiveAuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HierarchyVehicleController : Controller
    {
        private readonly ILogger<HierarchyVehicleController> _logger;
        private readonly IHierarchyVehicleService _hierarchyVehicleService;
        public HierarchyVehicleController(ILogger<HierarchyVehicleController> logger, IHierarchyVehicleService hierarchyVehicleService)
        {
            _logger = logger;
            _hierarchyVehicleService = hierarchyVehicleService;
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
        public async Task<IActionResult> Put([FromBody] List<HierarchyVehicle> hierarchies)
        {
            await Task.Delay(0);
            return NotFound();
        }

        [HttpPost]//creates new hierarchy
        public async Task Post([FromBody] List<HierarchyVehicle> hierarchies)
        {          

            var requestId = DateTime.UtcNow.ToFileTime().ToString();
            _logger.LogInformation("Request:{0} HttpReqeustType:{1}", requestId, HttpRequestType.GET);
            try
            {
                //await Task.Delay(0hierarchyUsers
               // await _hierarchyVehicleService.SaveOrganizationHierarchyVehicleAssignment(hierarchies);
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