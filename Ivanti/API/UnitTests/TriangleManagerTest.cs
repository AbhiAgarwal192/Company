using Ivanti.Constants;
using Ivanti.Manager;
using Ivanti.Manager.Contracts;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class TriangleManagerTest
    {
        private readonly ITriangleManager _triangleManager;
        public TriangleManagerTest()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"LengthOfTriangleSide", "10"},
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _triangleManager = new TriangleManager(configuration);
        }

        [Fact]
        public void WhenValidTriangleNameIsGiven_ThenCoordinatesIsReturned()
        {
            var result = _triangleManager.GetCoordinates("A1");

            Assert.NotEmpty(result);
            Assert.Equal(3,result.Count);
        }

        [Fact]
        public void WhenNullOrEmptyTriangleNameIsGiven_ThenEmptyListIsReturned()
        {
            var result = _triangleManager.GetCoordinates(string.Empty);

            Assert.Empty(result);
        }

        [Fact]
        public void WhenInvalidTriangleNameIsGiven_ThenEmptyListIsReturned()
        {
            var result = _triangleManager.GetCoordinates("a1");

            Assert.Empty(result);
        }

        [Fact]
        public void WhenValidTriangleCoordinatesIsGiven_ThenTriangleNameIsReturned()
        {
            var list = new List<int[]>();
            list.Add(new int[] { 0, 0 });
            list.Add(new int[] { 0, 10 });
            list.Add(new int[] { 10, 10 });

            var result = _triangleManager.GetTriangle(list);

            Assert.True(result.IsValid);
            Assert.Equal("A1", result.Value);
        }

        [Fact]
        public void WhenEmptyListIsGiven_ThenEmptyNameIsReturned()
        {
            var result = _triangleManager.GetTriangle(new List<int[]>());

            Assert.False(result.IsValid);
            Assert.Equal(Messages.CheckCoordinatesMessage, result.Value);
        }
    }
}
