using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Controllers;
using WebApi.IServices;
using WebApi.Services;
using Xunit;

namespace Bsm.WebApiUnitTest
{
    public class TemplatesControllerTest : IDisposable
    {
        Mock<ILogger<TemplatesController>> _mockLogger = null;
        Mock<TemplateService> _mockTemplateService = null;

        //Initialize
        public TemplatesControllerTest()
        {
            //mocking template service
            _mockTemplateService = new Mock<TemplateService>();

            //mocking logger
            _mockLogger = new Mock<ILogger<TemplatesController>>();
        }

		//Tear Down
		public void Dispose()
        {

        }

		[Fact]
		public void Get_ShouldReturnResults()
        {
            var templatesController = new TemplatesController(_mockTemplateService.Object, _mockLogger.Object);

            var results = templatesController.Get();
        }

        [Fact]
        public void Get_ShouldReturnNotFound()
        {
            var templatesController = new TemplatesController(_mockTemplateService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Get_WithID_ShouldReturnResults()
        {
            var templatesController = new TemplatesController(_mockTemplateService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Get_WithID_ShouldReturnNotFound()
        {
            var templatesController = new TemplatesController(_mockTemplateService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Post_NullTemplate_ShouldReturnBadRequest()
        {
            var templatesController = new TemplatesController(_mockTemplateService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Post_SaveFailed_ShouldReturnNotFound()
        {
            var templatesController = new TemplatesController(_mockTemplateService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Post_SaveSuccessful_ShouldReturnResults()
        {
            var templatesController = new TemplatesController(_mockTemplateService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Put_NullTemplate_ShouldReturnBadRequest()
        {
            var templatesController = new TemplatesController(_mockTemplateService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Put_SaveFailed_ShouldReturnNotFound()
        {
            var templatesController = new TemplatesController(_mockTemplateService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Put_SaveSuccessful_ShouldReturnResults()
        {
            var templatesController = new TemplatesController(_mockTemplateService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Delete_ShouldReturnNotFound()
        {
            var templatesController = new TemplatesController(_mockTemplateService.Object, _mockLogger.Object);
        }

        [Fact]
        public void Delete_ShouldReturnNoContentResult()
        {
            var templatesController = new TemplatesController(_mockTemplateService.Object, _mockLogger.Object);
        }
    }
}
