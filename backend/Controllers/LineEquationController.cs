using Microsoft.AspNetCore.Mvc;
using Veeb_TARpv23.Interfaces;
using Veeb_TARpv23.Models;
using Veeb_TARpv23.Services;

namespace LineEquationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LineEquationController : ControllerBase
    {
        private readonly ILogger<LineEquationController> _logger;

        public LineEquationController(ILogger<LineEquationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<object> Get()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            string[] a_parts = lines[0].Split(' ');
            string[] b_parts = lines[1].Split(' ');
            string[] x_parts = lines[2].Split(' ');

            float a_min = float.Parse(a_parts[0]);
            float a_max = float.Parse(a_parts[1]);
            float b_min = float.Parse(b_parts[0]);
            float b_max = float.Parse(b_parts[1]);
            float x_min = float.Parse(x_parts[0]);
            float x_max = float.Parse(x_parts[1]);

            float a = a_min;
            float b = b_min;

            ILineEquation lineEquation;
            string equationString;

            if (b == 0)
            {
                lineEquation = new LinearEquation(a);
                equationString = $"y = {a}x";
            }
            else
            {
                lineEquation = new AffineEquation(a, b);
                equationString = $"y = {a}x + {b}";
            }

            List<float> x_values = new List<float>();
            for (float x = x_min; x <= x_max; x += 1.0f)
            {
                x_values.Add(x);
            }

            var y_values = LineEquationCalculator.GetYValues(x_values.ToArray(), lineEquation);

            float approximateZero = EquationSolver.FindApproximateZero(lineEquation, x_min, x_max);

            return new
            {
                Equation = equationString,
                X_Values = x_values,
                Y_Values = y_values,
                ApproximateZero = approximateZero
            };
        }
    }
}