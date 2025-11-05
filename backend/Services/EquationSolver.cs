using Veeb_TARpv23.Interfaces;

namespace Veeb_TARpv23.Services
{
    public static class EquationSolver
    {
        public static float FindApproximateZero(ILineEquation lineEquation, float minX, float maxX, double tolerance = 1e-6)
        {
            float yMin = lineEquation.GetY(minX);
            float yMax = lineEquation.GetY(maxX);

            if (yMin * yMax > 0)
            {
                return float.NaN;
            }

            if (Math.Abs(yMin) < tolerance) return minX;
            if (Math.Abs(yMax) < tolerance) return maxX;

            float midX;
            do
            {
                midX = (minX + maxX) / 2.0f;
                float yMid = lineEquation.GetY(midX);

                if (Math.Abs(yMid) < tolerance)
                {
                    return midX;
                }
                else if (yMid * lineEquation.GetY(minX) < 0)
                {
                    maxX = midX;
                }
                else
                {
                    minX = midX;
                }
            } while (maxX - minX > tolerance);

            return (minX + maxX) / 2.0f;
        }
    }
}