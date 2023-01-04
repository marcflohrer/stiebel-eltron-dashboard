using System;
using System.Reflection;

namespace StiebelEltronDashboard.Models;

public partial class HeatPumpDataPerPeriod
{
    public static double GetPropertyValue(object src, string propName)
        => (double)(GetPropertyInfo(src, propName)?.GetValue(src, null) ?? throw new InvalidProgramException($"Property not found. {propName} in class {src.GetType()}"));

    private static PropertyInfo GetPropertyInfo(object src, string propName)
        => src.GetType()
        .GetProperty(propName);
}
