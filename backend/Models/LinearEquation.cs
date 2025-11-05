using Veeb_TARpv23.Interfaces;

namespace Veeb_TARpv23.Models
{
    public class LinearEquation : ILineEquation
    {
        private readonly float _multiplier;

        public LinearEquation(float multiplier)
        {
            _multiplier = multiplier;
        }

        public float GetY(float x)
        {
            return _multiplier * x;
        }
    }
}
