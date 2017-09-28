using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.IServices;
using WebApi.Services;
using WebApi.Models;
using System.Net.Http;
using System.Net;
using Microsoft.Extensions.Logging;
using static Bsm.WebApi.Constants.BusinessConstants;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers
{
    //[Produces("application/json")]
    [Route("Templates")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TemplatesController : Controller
    {
        private readonly ITemplateService _iTemplateService;
        private readonly ILogger _logger;
        public TemplatesController(ITemplateService iTemplateService,
            ILogger<TemplatesController> logger)
        {
            _iTemplateService = iTemplateService;
            _logger = logger;
            
        }
        
        [HttpGet]
        //[Authorize(ActiveAuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get()
        {            
            var requestId = DateTime.UtcNow.ToFileTime().ToString();
            try
            {
               
                _logger.LogInformation("Request:{0} HttpReqeustType:{1}", requestId, HttpRequestType.GET);
                var result = _iTemplateService.Get();

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

        [HttpGet("{GetTokenInfo}")]
        private IActionResult GetTokenInfo()
        {
            string token = "";
            string username = null;

            var simplePrinciple = GetPrincipal(token);
            var identity = simplePrinciple.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            if (!identity.IsAuthenticated)
                return NotFound();

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim?.Value;

            if (string.IsNullOrEmpty(username))
                return NotFound();

            // More validate to check whether username exists in system

            return NotFound();
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String("BSMwireless1234567890");

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }

            catch (Exception)
            {
                //should write log
                return null;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {          

            var requestId = DateTime.UtcNow.ToFileTime().ToString();
            try
            {

                _logger.LogInformation("Request:{0} HttpReqeustType:{1} For:{2}", requestId, HttpRequestType.GET,id);
                var result = _iTemplateService.Get(id);

                if (result == null)
                {
                    _logger.LogWarning("Response:{0}  For {1}", requestId,id);
                    return NotFound();
                }
                _logger.LogInformation("Response:{0}  For {1}", requestId, id);
                return new ObjectResult(result);
            }
            catch
            {
                _logger.LogError("Response:{0}  For {1}", requestId, id);
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Template template)
        {
            var requestId = DateTime.UtcNow.ToFileTime().ToString();
            try
            {
                _logger.LogInformation("Request:{0} HttpReqeustType:{1}", requestId, HttpRequestType.POST);
                if (template == null)
            {
                return BadRequest();
            }

            var result = _iTemplateService.Save(template);
                _logger.LogInformation("Response:{0}  is succeed", requestId);
                return new ObjectResult(result);
            }
            catch
            {
                _logger.LogError("Response:{0}  is failed", requestId);
                return NotFound();
            }
        }

       

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Template template)
        {
            var requestId = DateTime.UtcNow.ToFileTime().ToString();
            try
            {
                _logger.LogInformation("Request:{0} HttpReqeustType:{1} For:{2}", requestId, HttpRequestType.PUT, id);
                if (template == null)
                {
                    return BadRequest();
                }
                var result = _iTemplateService.Save(template);
                _logger.LogInformation("Response:{0}  is succeed", requestId);
                return new ObjectResult(result);
            }
            catch
            {
                _logger.LogError("Response:{0}  is failed", requestId);
                return NotFound();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var requestId = DateTime.UtcNow.ToFileTime().ToString();
            try
            {
                _logger.LogInformation("Request:{0} HttpReqeustType:{1} For:{2}", requestId, HttpRequestType.DELETE, id);
                _iTemplateService.Delete(id);
                _logger.LogInformation("Response:{0}  is succeed", requestId);
                return new NoContentResult();
            }
            catch
            {
                _logger.LogError("Response:{0}  is failed", requestId);
                return NotFound();
            }
        }
    }
}
