using Moq;
using WebApi.Controllers;
using Xunit;
using System.Linq;
using System;

namespace Bsm.WebApiUnitTest
{
    public class ValuesControllerTest : IDisposable
    {
        //Arrange
        private ValuesController _controller = null;

        //Test Initialize
        public ValuesControllerTest()
        {
            _controller = new ValuesController();
        }

        //Tear down
        public void Dispose()
        {
            _controller = null;
        }

        [Fact]
        public void Get_ShouldReturnTwoValues()
        {
            var result = _controller.Get();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetWithId_ShouldReturnValue()
        {
            int id = 1;
            var result = _controller.Get(id);

            Assert.Equal(result, "value");
        }

        [Fact]
        public void Post_ShouldAssert()
        {
            var value = "";
            _controller.Post(value);

            Assert.True(false);
        }

        [Fact]
        public void Put_ShouldAssert()
        {
            int id = 1;
            string value = "";

            _controller.Put(id, value);

            Assert.True(false);
        }

        [Fact]
        public void Delete()
        {
            int id = 1;

            _controller.Delete(id);

            Assert.True(false);
        }

    }
}
