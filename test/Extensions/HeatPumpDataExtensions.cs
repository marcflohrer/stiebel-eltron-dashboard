using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StiebelEltronDashboard.Models;

namespace StiebelEltronDashboardTests
{
    public static class HeatPumpDataExtensions
    {

        public static T SetMinDoubles<T>(this T heatPumpDataPerPeriod, double value) where T : new() => heatPumpDataPerPeriod.SetDoublesPerSuffix("Min", value);
        public static T SetMaxDoubles<T>(this T heatPumpDatum, double value) where T : new() => heatPumpDatum.SetDoublesPerSuffix("Max", value);
        public static T SetAverageDoubles<T>(this T heatPumpDatum, double value) where T : new() => heatPumpDatum.SetDoublesPerSuffix("Average", value);
        public static T SetStartDoubles<T>(this T heatPumpDatum, double value) where T : new() => heatPumpDatum.SetDoublesPerSuffix("Start", value);
        public static T SetEndDoubles<T>(this T heatPumpDatum, double value) where T : new() => heatPumpDatum.SetDoublesPerSuffix("End", value);
        public static T SetDeltaDoubles<T>(this T heatPumpDatum, double value) where T : new() => heatPumpDatum.SetDoublesPerSuffix("Delta", value);

        public static HeatPumpDataPerPeriod SetYear(this HeatPumpDataPerPeriod heatPumpDataPerPeriod, int value)
        {
            heatPumpDataPerPeriod.Year = value;
            return heatPumpDataPerPeriod;
        }
        public static HeatPumpDataPerPeriod SetPeriodKind(this HeatPumpDataPerPeriod heatPumpDataPerPeriod, string value)
        {
            heatPumpDataPerPeriod.PeriodKind = value;
            return heatPumpDataPerPeriod;
        }
        public static HeatPumpDataPerPeriod SetPeriodNumber(this HeatPumpDataPerPeriod heatPumpDataPerPeriod, int value)
        {
            heatPumpDataPerPeriod.PeriodNumber = value;
            return heatPumpDataPerPeriod;
        }
        public static HeatPumpDataPerPeriod SetFirst(this HeatPumpDataPerPeriod heatPumpDataPerPeriod, DateTime value)
        {
            heatPumpDataPerPeriod.First = value;
            return heatPumpDataPerPeriod;
        }
        public static HeatPumpDataPerPeriod SetLast(this HeatPumpDataPerPeriod heatPumpDataPerPeriod, DateTime value)
        {
            heatPumpDataPerPeriod.Last = value;
            return heatPumpDataPerPeriod;
        }

        public static T SetDoublesPerSuffix<T>(this T datum, string suffix, double value) where T : new()
        {
            IEnumerable<PropertyInfo> enumerable()
            {
                foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (propertyInfo.GetValue(new T(), null)?.GetType()?.FullName == typeof(System.Double).FullName)
                    {
                        if (propertyInfo?.Name?.EndsWith(suffix) ?? false)
                        {
                            yield return propertyInfo;
                        }
                    }
                }
            }

            foreach (var doubleProperty in ((Func<IEnumerable<PropertyInfo>>)(() => enumerable().ToList()))())
            {
                ((Action<T, PropertyInfo, double>)((d, propertyInfo, value) =>
                    propertyInfo.SetValue(d, value, null)))(datum, doubleProperty, value);
            }
            return datum;
        }

        public static T SetDoublesPerName<T>(this T datum, string name, double value) where T : new()
        {
            IEnumerable<PropertyInfo> enumerable()
            {
                foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (propertyInfo.GetValue(new T(), null)?.GetType()?.FullName == typeof(System.Double).FullName)
                    {
                        if (propertyInfo?.Name == (name))
                        {
                            yield return propertyInfo;
                        }
                    }
                }
            }

            foreach (var doubleProperty in ((Func<IEnumerable<PropertyInfo>>)(() => enumerable().ToList()))())
            {
                ((Action<T, PropertyInfo, double>)((d, propertyInfo, value) =>
                    propertyInfo.SetValue(d, value, null)))(datum, doubleProperty, value);
            }
            return datum;
        }

        public static T SetStringPerName<T>(this T datum, string name, string value) where T : new()
        {
            var typeToSet = typeof(System.String);
            IEnumerable<PropertyInfo> enumerable()
            {
                foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (propertyInfo.GetValue(new T(), null)?.GetType()?.FullName == typeToSet.FullName)
                    {
                        if (propertyInfo?.Name == (name))
                        {
                            yield return propertyInfo;
                        }
                    }
                }
            }

            foreach (var stringProperty in ((Func<IEnumerable<PropertyInfo>>)(() => enumerable().ToList()))())
            {
                ((Action<T, PropertyInfo, string>)((d, propertyInfo, value) =>
                    propertyInfo.SetValue(d, value, null)))(datum, stringProperty, value);
            }
            return datum;
        }

        public static HeatPumpDatum SetDoubles(this HeatPumpDatum heatPumpDatum, double value)
        {

            foreach (var doubleProperty in ((Func<IEnumerable<PropertyInfo>>)(() =>
                    (from propertyInfo in typeof(HeatPumpDatum).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                     where propertyInfo.GetValue(new HeatPumpDatum(), null).GetType().FullName == typeof(System.Double).FullName
                     select propertyInfo).ToList()))())
            {
                ((Action<HeatPumpDatum, PropertyInfo, double>)((heatPumpDatum, propertyInfo, value) =>
                    propertyInfo.SetValue(heatPumpDatum, value, null)))(heatPumpDatum, doubleProperty, value);
            }
            return heatPumpDatum;
        }

        public static HeatPumpDataPerPeriod SetDoubles(this HeatPumpDataPerPeriod heatPumpDataPerPeriod, double value)
        {

            foreach (var doubleProperty in ((Func<IEnumerable<PropertyInfo>>)(() =>
                    (from propertyInfo in typeof(HeatPumpDataPerPeriod).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                     where propertyInfo.GetValue(new HeatPumpDataPerPeriod(), null).GetType().FullName == typeof(System.Double).FullName
                     select propertyInfo).ToList()))())
            {
                ((Action<HeatPumpDataPerPeriod, PropertyInfo, double>)((hpd, propertyInfo, value) =>
                    propertyInfo.SetValue(hpd, value, null)))(heatPumpDataPerPeriod, doubleProperty, value);
            }
            return heatPumpDataPerPeriod;
        }

        public static T SetDateTimes<T>(this T heatPumpDatum, DateTime value) where T : new()
        {
            IEnumerable<PropertyInfo> enumerable()
            {
                foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (propertyInfo.GetValue(new T(), null)?.GetType()?.FullName == typeof(System.DateTime).FullName)
                    {
                        yield return propertyInfo;
                    }
                }
            }

            foreach (var dateTimeProperty in ((Func<IEnumerable<PropertyInfo>>)(() => enumerable().ToList()))())
            {
                ((Action<T, PropertyInfo, DateTime>)((heatPumpDatum, propertyInfo, value) =>
                    propertyInfo.SetValue(heatPumpDatum, value, null)))(heatPumpDatum, dateTimeProperty, value);
            }
            return heatPumpDatum;
        }

        public static T SetDateTimePerName<T>(this T heatPumpDatum, DateTime value, string PropertyName) where T : new()
        {
            IEnumerable<PropertyInfo> enumerable()
            {
                foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (propertyInfo.GetValue(new T(), null)?.GetType()?.FullName == typeof(System.DateTime).FullName)
                    {
                        if (propertyInfo?.Name == (PropertyName))
                        {
                            yield return propertyInfo;
                        }
                    }
                }
            }

            foreach (var dateTimeProperty in ((Func<IEnumerable<PropertyInfo>>)(() => enumerable().ToList()))())
            {
                ((Action<T, PropertyInfo, DateTime>)((heatPumpDatum, propertyInfo, value) =>
                    propertyInfo.SetValue(heatPumpDatum, value, null)))(heatPumpDatum, dateTimeProperty, value);
            }
            return heatPumpDatum;
        }

        public static T SetDateUpdated<T>(this T heatPumpDatum, DateTime value) where T : new()
            => heatPumpDatum.SetDateTimePerName(value, "DateUpdated");
        public static T SetDateCreated<T>(this T heatPumpDatum, DateTime value) where T : new()
            => heatPumpDatum.SetDateTimePerName(value, "DateCreated");
        public static T SetPeriodStart<T>(this T heatPumpDatum, DateTime value) where T : new()
            => heatPumpDatum.SetDateTimePerName(value, "PeriodStart");
        public static T SetPeriodEnd<T>(this T heatPumpDatum, DateTime value) where T : new()
            => heatPumpDatum.SetDateTimePerName(value, "PeriodEnd");
    }
}