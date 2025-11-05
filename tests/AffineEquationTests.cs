using Veeb_TARpv23.Models;
using Xunit;
using Assert = Xunit.Assert;

namespace tests
{
    public class AffineEquationTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 3)]
        [InlineData(-1, -3)]
        [InlineData(10, 30)]
        public void GetY_ForLineYEquals3X_ReturnsCorrectY(float x, float expectedY)
        {
            AffineEquation line = new AffineEquation(3, 0);

            float actualY = line.GetY(x);

            Assert.Equal(expectedY, actualY);
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(1, 5)]
        [InlineData(-1, -1)]
        [InlineData(10, 32)]
        public void GetY_ForLineYEquals3XPlus2_ReturnsCorrectY(float x, float expectedY)
        {
            AffineEquation line = new AffineEquation(3, 2);

            float actualY = line.GetY(x);

            Assert.Equal(expectedY, actualY);
        }
    }
}
