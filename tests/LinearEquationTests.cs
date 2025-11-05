using Veeb_TARpv23.Models;
using Xunit;
using Assert = Xunit.Assert;

namespace LineEquationApi.Tests
{
    public class LinearEquationTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 3)]
        [InlineData(-1, -3)]
        [InlineData(10, 30)]
        public void GetY_ForLineYEquals3X_ReturnsCorrectY(float x, float expectedY)
        {
            LinearEquation line = new LinearEquation(3);

            float actualY = line.GetY(x);

            Assert.Equal(expectedY, actualY);
        }
    }
}