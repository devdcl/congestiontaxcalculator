using System.Reflection;

namespace Congestion.TaxCalculator;

public class VehicleFactory
{
    public static IVehicle GetVehicleInstanceFromType(string vehicleTypeName)
    {
        // Get the current assembly
        var assembly = Assembly.GetExecutingAssembly();
        
        // Find all types implementing IVehicle
        var vehicleTypes = assembly.GetTypes()
            .Where(t => typeof(IVehicle).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        // Find the type matching the provided name (case-insensitive)
        var matchingType = vehicleTypes.FirstOrDefault(t => 
            string.Equals(t.Name, vehicleTypeName, StringComparison.OrdinalIgnoreCase));
        
        if (matchingType == null)
        {
            throw new ArgumentException($"No vehicle type found for name: {vehicleTypeName}");
        }

        // Create an instance of the matching type
        return (IVehicle)Activator.CreateInstance(matchingType)!;
    }
}