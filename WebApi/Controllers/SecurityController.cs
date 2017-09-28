using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bsm.WebApi.Constants;
using Microsoft.Extensions.Options;
using Bsm.WebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using WebApi.IServices;
using Microsoft.Extensions.Logging;
using static Bsm.WebApi.Constants.BusinessConstants;

namespace Bsm.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("Security")]
    public class SecurityController : Controller
    {
        private readonly IOptions<SecuritySettings> _securitySettings;
        private readonly ISecurityService _securityService;
        ILogger<SecurityController> _logger;

        public SecurityController(IOptions<SecuritySettings> securitySettings, ISecurityService securityService, ILogger<SecurityController> logger)
        {
            _securitySettings = securitySettings;
            _securityService = securityService;
            _logger = logger;
        }


        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] User model)
        {
            var requestId = DateTime.UtcNow.ToFileTime().ToString();
            try
            {
                if (model == null)
                {
                    _logger.LogError("Request:{0} HttpReqeustType:{1} Request:{2} MessageType:{3} ErrorType:{4}", requestId, HttpRequestType.POST, RequestName.SECURITY_TOKEN, MessageType.ERR, ErrorType.INVALID_USER_NAME);
                    return BadRequest();
                }
                _logger.LogInformation("Request:{0} HttpReqeustType:{1} Request:{2} UserName:{3}", requestId, HttpRequestType.POST, RequestName.SECURITY_TOKEN, model.UserName);
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Request:{0} HttpReqeustType:{1} Request:{2} MessageType:{3} ErrorType:{4}", requestId, HttpRequestType.POST, RequestName.SECURITY_TOKEN, MessageType.ERR, ErrorType.INVALID_MODEL_STATE);
                    return BadRequest();
                }

                var user = await _securityService.ValidateUser(model.UserName, model.Password);

                if (user == null)
                {
                    _logger.LogError("Request:{0} HttpReqeustType:{1} Request:{2}  UserName:{3} MessageType:{4} ErrorType:{5}", requestId, HttpRequestType.POST, RequestName.SECURITY_TOKEN, model.UserName, MessageType.ERR, ErrorType.INVALID_USERNAME_PASSWORD);
                    return BadRequest();
                }

                var token = GetJwtSecurityToken(user);

                _logger.LogInformation("Request:{0} HttpReqeustType:{1} Request:{2} UserName:{3} MessageType:{4} InfoType:{5}", requestId, HttpRequestType.POST, RequestName.SECURITY_TOKEN, user.UserId, MessageType.INFO, InfoType.TOKEN_ISSUED);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });

            }
            catch (Exception ex)
            {
                _logger.LogError("Request:{0} HttpReqeustType:{1} Request:{2} MessageType:{3} ErrorType:{4} ActualError:{5}", requestId, HttpRequestType.POST, RequestName.SECURITY_TOKEN, MessageType.ERR, ErrorType.UNHANDLED_ERROR, ex.ToString());
                return BadRequest();
            }
        }

        private JwtSecurityToken GetJwtSecurityToken(User user)
        {
            //var userClaims = await _userManager.GetClaimsAsync(user);

            {
                return new JwtSecurityToken(
                    issuer: _securitySettings.Value.SiteUrl,
                    audience: _securitySettings.Value.SiteUrl,
                     //claims: GetTokenClaims(user).Union(userClaims),
                     claims: GetTokenClaims(user),
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_securitySettings.Value.Key)), SecurityAlgorithms.HmacSha256)
                );
            }

        }

        private static IEnumerable<Claim> GetTokenClaims(User user)
        {
            return new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
    };


            //return new List<Claim>

        }
    }
}