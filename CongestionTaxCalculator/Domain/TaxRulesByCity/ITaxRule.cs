namespace Congestion.TaxCalculator.TaxRulesByCity;

public interface ITaxRule
{
    int GetTollFee(DateTime date, IVehicle vehicle);
}