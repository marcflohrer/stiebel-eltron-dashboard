using System;
using System.Collections.Generic;
using System.Linq;

namespace StiebelEltronApiServer.Services {
    public class UnitService : IUnitService {
        public IList<UnitInformation> ConversionTable = new List<UnitInformation> () {
            new UnitInformation ("Wh", new Dictionary<string, double> () { { "GWh", Math.Pow (10, 9) } }),
            new UnitInformation ("Wh", new Dictionary<string, double> () { { "MWh", Math.Pow (10, 6) } }),
            new UnitInformation ("Wh", new Dictionary<string, double> () { { "kWh", Math.Pow (10, 3) } }),            
            new UnitInformation ("Wh", new Dictionary<string, double> () { { "Wh", Math.Pow (10, 0) } }),
            new UnitInformation ("Hz", new Dictionary<string, double> () { { "Ghz", Math.Pow (10, 6) } }),
            new UnitInformation ("Hz", new Dictionary<string, double> () { { "Mhz", Math.Pow (10, 6) } }),
            new UnitInformation ("Hz", new Dictionary<string, double> () { { "kHz", Math.Pow (10, 3) } }),
            new UnitInformation ("Hz", new Dictionary<string, double> () { { "Hz", Math.Pow (10, 0) } }),
            new UnitInformation ("Hz", new Dictionary<string, double> () { { "mHz", Math.Pow (10, -3) } }),          
            new UnitInformation ("V", new Dictionary<string, double> () { { "GV", Math.Pow (10, 9) } }),
            new UnitInformation ("V", new Dictionary<string, double> () { { "MV", Math.Pow (10, 6) } }),
            new UnitInformation ("V", new Dictionary<string, double> () { { "kV", Math.Pow (10, 3) } }),
            new UnitInformation ("V", new Dictionary<string, double> () { { "V", Math.Pow (10, 0) } }),
            new UnitInformation ("V", new Dictionary<string, double> () { { "mV", Math.Pow (10, -3) } }),
            new UnitInformation ("l/min", new Dictionary<string, double> () { { "l/min", Math.Pow (10, 0) } }),
            new UnitInformation ("°C", new Dictionary<string, double> () { { "°C", Math.Pow (10, 0) } }),
            new UnitInformation ("h", new Dictionary<string, double> () { { "h", Math.Pow (10, 0) } }),
        };

        public double GetBaseUnitValue ((double value, string unit) input) {
            var value = input.value;
            var unit = input.unit;
            if(ConversionTable.Select(list => list.baseUnit).Contains(unit)){
                return value;
            }
            if(!ConversionTable.Select(l => l.conversionTable).Where(l => l.Keys.Where(key => (key == unit)).Any()).Any()){
                Console.WriteLine($"Error: unknown unit {unit}. Cannot convert this unit to its base unit.");
            }
            var conversionRate = ConversionTable
                                    .Select(l => l.conversionTable)
                                    .Where(l => l.Keys.Where(key => (key == unit))
                                    .Any()).FirstOrDefault()
                                    .FirstOrDefault()
                                    .Value;
            return value * conversionRate;
        }
    }

    public record UnitInformation (string baseUnit, IDictionary<string, double> conversionTable);
}