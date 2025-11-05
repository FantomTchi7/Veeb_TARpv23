using Veeb_TARpv23.Interfaces;

namespace Veeb_TARpv23.Models
{
    public class AffineEquation : ILineEquation
    {
        private readonly float _multiplier;
        private readonly float _freeMember;

        public AffineEquation(float multiplier, float freeMember)
        {
            _multiplier = multiplier;
            _freeMember = freeMember;
        }

        public float GetY(float x)
        {
            return _multiplier * x + _freeMember;
        }
    }
}