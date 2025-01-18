namespace Congestion.TaxCalculator;

public interface ICongestionTaxCalculator
{
    int GetTax(IVehicle vehicle, DateTime[] dates);
}