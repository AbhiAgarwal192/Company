using Ivanti.Controllers;
using Ivanti.Manager.Contracts;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests
{
    public class TriangleControllerTest
    {
        private readonly TriangleController _triangleController;
        private readonly Mock<ITriangleManager> _triangleManagerMock;
        
        public TriangleControllerTest()
        {
            var loggerMock = new Mock<ILogger<TriangleController>>();
            _triangleManagerMock = new Mock<ITriangleManager>();
            _triangleController = new TriangleController(_triangleManagerMock.Object,loggerMock.Object);
        }

        [Fact]
        public void WhenEmptyTriangleNameIsGiven_ThenBadRequestIsReturned()
        {
            var result = _triangleController.GetTriangleCoordinates(string.Empty);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void WhenInValidTriangleNameIsGiven_Then200StatusCodeIsReturnedWithNoResult()
        {
            var list = new List<int[]>();
            _triangleManagerMock.Setup(_call => _call.GetCoordinates(It.IsAny<string>())).Returns(list);

            var result = _triangleController.GetTriangleCoordinates("A100");

            Assert.IsType<OkObjectResult>(result);
            var output = (result as OkObjectResult)?.Value as List<int[]>;
            Assert.Empty(output);
        }

        [Fact]
        public void WhenValidTriangleNameIsGiven_Then200StatusCodeIsReturnedWithCoordinates()
        {
            var list = new List<int[]>();
            list.Add(new int[] { 0 , 0 });
            list.Add(new int[] { 0 , 10 });
            list.Add(new int[] { 10 , 10 });
            _triangleManagerMock.Setup(_call => _call.GetCoordinates(It.IsAny<string>())).Returns(list);

            var result = _triangleController.GetTriangleCoordinates("A1");

            Assert.IsType<OkObjectResult>(result);
            var output = (result as OkObjectResult)?.Value as List<int[]>;
            Assert.NotEmpty(output);
            Assert.Equal(3,output.Count);

        }

        [Fact]
        public void WhenNullOrEmptyTriangleCoordinatesIsGiven_ThenBadRequestIsReturned()
        {
            var list = new List<int[]>();
            var result = _triangleController.GetTriangleName(list);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void WhenLessThan3TriangleCoordinatesIsGiven_ThenBadRequestIsReturned()
        {
            _triangleManagerMock.Setup(_call => _call.GetTriangle(It.IsAny<List<int[]>>())).Returns(string.Empty);

            var list = new List<int[]>();
            list.Add(new int[] { 0 , 0 });
            var result = _triangleController.GetTriangleName(list);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void WhenValidTriangleCoordinatesIsGiven_Then200IsReturnedWithTriangleName()
        {
            _triangleManagerMock.Setup(_call => _call.GetTriangle(It.IsAny<List<int[]>>())).Returns("A1");

            var list = new List<int[]>();
            list.Add(new int[] { 0, 0 });
            list.Add(new int[] { 0, 10 });
            list.Add(new int[] { 10, 10 });

            var result = _triangleController.GetTriangleName(list);

            Assert.IsType<OkObjectResult>(result);
            var output = result as OkObjectResult;
            Assert.Equal("A1",output.Value);
        }
    }
}
