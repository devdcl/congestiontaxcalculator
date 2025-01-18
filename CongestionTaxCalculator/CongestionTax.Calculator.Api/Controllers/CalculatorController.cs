using Congestion.TaxCalculator;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTax.Calculator.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController(ICongestionTaxCalculator congestionTaxCalculator) : ControllerBase
{
    [HttpPost("congestionTax/{vehicleType}")]
    public async Task<IActionResult> CalculateCongestionTax(string vehicleType, [FromBody]string[] tollDates)
    {
        try
        {
            var vehicleTypeObj = VehicleFactory.GetVehicleInstanceFromType(vehicleType);
            var taxResult = await Task.FromResult(congestionTaxCalculator.GetTax(vehicleTypeObj, tollDates.Select(DateTime.Parse).ToArray()));
            return Ok(taxResult);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}