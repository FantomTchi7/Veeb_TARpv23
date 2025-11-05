using Veeb_TARpv23.Interfaces;

namespace Veeb_TARpv23.Services
{
    public static class LineEquationCalculator
    {
        public static float[] GetYValues(float[] xValues, ILineEquation lineEquation)
        {
            float[] yValues = new float[xValues.Length];
            for (int i = 0; i < xValues.Length; i++)
            {
                yValues[i] = lineEquation.GetY(xValues[i]);
            }
            return yValues;
        }
    }
}