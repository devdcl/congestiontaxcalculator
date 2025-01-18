using Congestion.TaxCalculator;
using Congestion.TaxCalculator.TaxRulesByCity;

public class CongestionTaxCalculator(ITaxRule taxRule) : ICongestionTaxCalculator
{
    /// <summary>
    /// Calculate the total toll fee for one day
    /// </summary>
    /// <param name="vehicle">the vehicle</param>
    /// <param name="dates">date and time of all passes on one day</param>
    /// <returns>the total congestion tax for that day</returns>
    public int GetTax(IVehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = taxRule.GetTollFee(date, vehicle);
            int tempFee = taxRule.GetTollFee(intervalStart, vehicle);

            long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            long minutes = diffInMillies / 1000 / 60;

            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    
}