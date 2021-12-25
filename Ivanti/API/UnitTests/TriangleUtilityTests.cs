using Ivanti.Utility;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class TriangleUtilityTests
    {
        [Fact]
        public void WhenValidCoordinatesArePassed_ThenReturnTrue()
        {
            var list = new List<int[]>();
            list.Add(new int[] { 0, 0 });
            list.Add(new int[] { 0, 10 });
            list.Add(new int[] { 10, 10 });

            var isValid = TriangleUtility.IsValidRighAngledTriangle(list);

            Assert.True(isValid);
        }

        [Theory]
        [InlineData(0,0,0,0,0,0)]
        [InlineData(0, 0, 0, 0, 0, 100)]
        [InlineData(1, 1, 2, 2, 3, 3)]
        public void WhenInValidCoordinatesArePassed_ThenReturnFalse(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            var list = new List<int[]>();
            list.Add(new int[] { x1, y1 });
            list.Add(new int[] { x2, y2 });
            list.Add(new int[] { x3, y3 });

            var isValid = TriangleUtility.IsValidRighAngledTriangle(list);

            Assert.False(isValid);
        }
    }
}
