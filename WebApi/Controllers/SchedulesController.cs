using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bsm.WebApi.IServices;
using Microsoft.Extensions.Logging;
using static Bsm.WebApi.Constants.BusinessConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Bsm.WebApi.Controllers
{
    //[Produces("application/json")]
    [Route("Schedules")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SchedulesController : Controller
    {
        private readonly IScheduleService _scheduleService;       
        private readonly ILogger<SecurityController> _logger;

        public SchedulesController(IScheduleService scheduleService, ILogger<SecurityController> logger)
        {
            _scheduleService = scheduleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult>Get()
        {
            var requestId = DateTime.UtcNow.ToFileTime().ToString();
            try
            {

                _logger.LogInformation("Request:{0} HttpReqeustType:{1}", requestId, HttpRequestType.GET);
                var result = await _scheduleService.GetAllScheduleTask();

                if (result == null)
                {
                    _logger.LogWarning("Response:{0}", requestId);
                    return NotFound();
                }
                _logger.LogInformation("Response:{0}", requestId);
                return new ObjectResult(result);
            }
            catch
            {
                _logger.LogError("Response:{0}", requestId);
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var requestId = DateTime.UtcNow.ToFileTime().ToString();
            try
            {

                _logger.LogInformation("Request:{0} HttpReqeustType:{1}", requestId, HttpRequestType.GET);
                var result = await _scheduleService.GetAllScheduleTaskByOrganization(id);

                if (result == null)
                {
                    _logger.LogWarning("Response:{0}", requestId);
                    return NotFound();
                }
                _logger.LogInformation("Response:{0}", requestId);
                return new ObjectResult(result);
            }
            catch
            {
                _logger.LogError("Response:{0}", requestId);
                return NotFound();
            }
        }
    }
}