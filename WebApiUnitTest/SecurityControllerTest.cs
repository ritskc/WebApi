using Bsm.WebApi.Controllers;
using System;
using Xunit;
using Moq;
using WebApi.Services;
using Bsm.WebApi.Constants;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Bsm.WebApi.Models;
using System.Threading.Tasks;

namespace Bsm.WebApiUnitTest
{
    public class SecurityControllerTest : IDisposable
    {
        private Mock<IOptions<SecuritySettings>> _mockSecuritySettings = null;
        private Mock<SecurityService> _mockSecurityService = null;
        private Mock<ILogger<SecurityController>> _mockLogger = null;

        public SecurityControllerTest ()
        {
            _mockSecuritySettings = new Mock<IOptions<SecuritySettings>>();

            //mocking security service
            _mockSecurityService = new Mock<SecurityService>(null);


            //mocking logger
            _mockLogger = new Mock<ILogger<SecurityController>>();
        }

        public void Dispose()
        {

        }

        [Fact]
        public void Token_NullModel_ShouldReturnBadRequest()
        {
            var securityController = new SecurityController(_mockSecuritySettings.Object, _mockSecurityService.Object, _mockLogger.Object);

            var response = securityController.Token(null);

            var result = response.Result.ToString();

            Assert.True(result.Contains("BadRequest"));
        }

        [Fact]
        //TODO: await asyn task -- part of integration testing
        public void Token_NullUser_ShouldReturnBadRequest()
        {
            //User user = new User();

            //user.UserName = "michaelv";
            //user.Password = "test123";

            //var securityController = new SecurityController(_mockSecuritySettings.Object, _mockSecurityService.Object, _mockLogger.Object);

            //var response = securityController.Token(user);
            //var result = response.Result.ToString();
            //Assert.True(result.Contains("BadRequest"));
        }
    }
}
