using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StiebelEltronApiServer.Models;

namespace StiebelEltronApiServerTests {
    public static class HeatPumpDataExtensions {
        public static HeatPumpDatum SetDoubles (this HeatPumpDatum heatPumpDatum, double value) {

            foreach (var doubleProperty in ((Func<IEnumerable<PropertyInfo>>)(() =>
            (from propertyInfo in typeof(HeatPumpDatum).GetProperties(BindingFlags.Instance | BindingFlags.Public)
             where propertyInfo.GetValue(new HeatPumpDatum(), null).GetType().FullName == typeof(System.Double).FullName
             select propertyInfo).ToList()))()) {
                ((Action<HeatPumpDatum, PropertyInfo, double>)((heatPumpDatum, propertyInfo, value) => 
                propertyInfo.SetValue(heatPumpDatum, value, null)))(heatPumpDatum, doubleProperty, value);
            }
            return heatPumpDatum;
        }
        
        public static HeatPumpDatum SetDateTimes (this HeatPumpDatum heatPumpDatum, DateTime value) {
            foreach (var dateTimeProperty in ((Func<IEnumerable<PropertyInfo>>)(() =>
            (from propertyInfo in typeof(HeatPumpDatum).GetProperties(BindingFlags.Instance | BindingFlags.Public)
             where propertyInfo.GetValue(new HeatPumpDatum(), null).GetType().FullName == typeof(System.DateTime).FullName
             select propertyInfo).ToList()))()) {
                ((Action<HeatPumpDatum, PropertyInfo, DateTime>)((heatPumpDatum, propertyInfo, value) =>
                 propertyInfo.SetValue(heatPumpDatum, value, null)))(heatPumpDatum, dateTimeProperty, value);
            }
            return heatPumpDatum;
        }
    }
}