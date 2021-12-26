using Ivanti.Constants;
using Ivanti.Manager;
using Ivanti.Manager.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class TriangleServiceTest
    {
        private readonly ITriangleService _triangleService;
        public TriangleServiceTest()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"LengthOfTriangleSide", "10"},
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _triangleService = new TriangleService(configuration);
        }

        [Fact]
        public void WhenTriangleLengthIsZero_ThenThrowArgumentException()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"LengthOfTriangleSide", "0"},
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var triangleService = 

            Assert.Throws<ArgumentException>(() => new TriangleService(configuration));
        }

        [Fact]
        public void WhenValidTriangleNameIsGiven_ThenCoordinatesIsReturned()
        {
            var result = _triangleService.GetCoordinates("A1");

            Assert.NotEmpty(result);
            Assert.Equal(3,result.Count);
        }

        [Fact]
        public void WhenNullOrEmptyTriangleNameIsGiven_ThenEmptyListIsReturned()
        {
            var result = _triangleService.GetCoordinates(string.Empty);

            Assert.Empty(result);
        }

        [Fact]
        public void WhenInvalidTriangleNameIsGiven_ThenEmptyListIsReturned()
        {
            var result = _triangleService.GetCoordinates("a1");

            Assert.Empty(result);
        }

        [Fact]
        public void WhenValidTriangleCoordinatesIsGiven_ThenTriangleNameIsReturned()
        {
            var list = new List<int[]>();
            list.Add(new int[] { 0, 0 });
            list.Add(new int[] { 0, 10 });
            list.Add(new int[] { 10, 10 });

            var result = _triangleService.GetTriangle(list);

            Assert.True(result.IsValid);
            Assert.Equal("A1", result.Value);
        }

        [Fact]
        public void WhenEmptyListIsGiven_ThenEmptyNameIsReturned()
        {
            var result = _triangleService.GetTriangle(new List<int[]>());

            Assert.False(result.IsValid);
            Assert.Equal(Messages.CheckCoordinatesMessage, result.Value);
        }
    }
}
