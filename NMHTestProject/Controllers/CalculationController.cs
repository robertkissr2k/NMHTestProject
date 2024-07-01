using Microsoft.AspNetCore.Mvc;
using NMHTestProject.Dto;
using NMHTestProject.Services;

namespace NMHTestProject.Controllers
{
    public class CalculationController : Controller
    {
        private readonly ICalculationService _calculationService;

        public CalculationController(ICalculationService calculationService)
        {
            _calculationService = calculationService;
        }

        [HttpPost("{key}")]
        public async Task<IActionResult> Calculation([FromRoute] int key, [FromBody] CalculationInput input)
        {
            return Ok(await _calculationService.CalculateAsync(key, input));
        }
    }
}
