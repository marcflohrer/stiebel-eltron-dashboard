using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace stiebel_eltron_dashboard_tests.Extensions
{
    public static class HeatPumpDataPerPeriodExtensions
    {
        public static List<string> ChangedProperties<T>(this T originalObject, T changedObject)
            => (from PropertyInfo property in originalObject.GetType().GetProperties()
                let originalValue = property.GetValue(originalObject, null)
                let changedValue = property.GetValue(changedObject, null)
                where !object.Equals(originalValue, changedValue)
                select property.Name).ToList();

        public static object GetValueOfProperty<T>(this T originalObject, string propertyName)
            => originalObject.GetType()
                .GetProperties()
                .FirstOrDefault(property => property.Name == propertyName)?
                .GetValue(originalObject, null);
    }
}
