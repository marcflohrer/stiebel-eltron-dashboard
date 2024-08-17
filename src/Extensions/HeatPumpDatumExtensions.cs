using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using StiebelEltronDashboard.Models;

namespace StiebelEltronDashboard.Extensions
{
    public static class HeatPumpDatumExtensions
    {
        public static double? GetMinForMetric(this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector)
        {
            if (!heatPumpData.Any())
            {
                Log.Debug("null <-- GetMinForMetric!");
                return null;
            }
            var min = Double.MaxValue;
            var metrics = heatPumpData.Select(selector);
            if (metrics.Any())
            {
                foreach (var metric in from metric in metrics
                                       where metric < min
                                       select metric)
                {
                    min = metric;
                }
            }
            return Math.Round(min, 1);
        }

        public static double? GetMaxForMetric(this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector)
        {
            if (!heatPumpData.Any())
            {
                Log.Debug("null <-- GetAverageForMetric!");
                return null;
            }
            var max = Double.MinValue;
            var metrics = heatPumpData.Select(selector);
            if (metrics.Any())
            {
                foreach (var metric in from metric in metrics
                                       where metric > max
                                       select metric)
                {
                    max = metric;
                }
            }
            return Math.Round(max, 1);
        }

        public static double? GetAverageForMetric(this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector)
        {
            if (!heatPumpData.Any())
            {
                Log.Debug("<-- GetAverageForMetric!");
                return null;
            }
            var sum = 0.0;
            var metrics = heatPumpData.Select(selector);
            if (metrics.Any())
            {
                foreach (var metric in metrics)
                {
                    sum += metric;
                }
            }
            else
            {
                Log.Error("null <-- !Any() <-- GetAverageForMetric!");
            }
            var result = sum / heatPumpData.Count();
            return Math.Round(result, 1);
        }

        public static double? GetStartForMetric(this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector)
        {
            if (!heatPumpData.Any())
            {
                Log.Error("null <-- GetStartForMetric!");
                return null;
            }
            var startTime = heatPumpData.Select(h => h.DateCreated).Min();
            var startHeatPumpData = heatPumpData.Where(h => h.DateCreated == startTime);
            var startMetric = startHeatPumpData.Select(selector);
            if (startMetric == null)
            {
                Log.Error("null <-- startMetric <-- GetStartForMetric!");
            }
            var result = startMetric?.FirstOrDefault() ?? 0.0;
            return Math.Round(result, 1);
        }

        public static double? GetEndForMetric(this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector)
        {
            if (!heatPumpData.Any())
            {
                Log.Error("null <-- GetEndForMetric!");
                return null;
            }
            var endTime = heatPumpData.Select(h => h.DateCreated).Max();
            var endHeatPumpData = heatPumpData.Where(h => h.DateCreated == endTime);
            var endMetric = endHeatPumpData.Select(selector);
            if (endMetric == null)
            {
                Log.Error("null <-- endMetric <-- GetEndForMetric!");
            }
            var result = endMetric?.FirstOrDefault() ?? 0.0;
            return Math.Round(result, 1);
        }

        public static double? GetDeltaForMetric(this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector)
        {
            if (!heatPumpData.Any())
            {
                Log.Error("null <-- GetDeltaForMetric!");
                return null;
            }
            var min = GetMinForMetric(heatPumpData, selector);
            var max = GetMaxForMetric(heatPumpData, selector);
            var delta = max - min;
            return Math.Round(delta ?? 0, 1);
        }

        public static DateTime GetFirst(this IEnumerable<HeatPumpDatum> heatPumpData)
        {
            var first = DateTimeExtensions.EnsureDateTimeIsUtc(DateTime.MaxValue);
            var creationDates = heatPumpData.Select(hpd => hpd.DateCreated);
            if (creationDates.Any())
            {
                foreach (var creationDate in creationDates)
                {
                    if (first > creationDate)
                    {
                        first = DateTimeExtensions.EnsureDateTimeIsUtc(creationDate);
                    }
                }
            }
            else
            {
                Log.Debug("!Any() <-- GetDeltaForMetric!");
            }
            return DateTimeExtensions.EnsureDateTimeIsUtc(first);
        }

        public static DateTime GetLast(this IEnumerable<HeatPumpDatum> heatPumpData)
        {
            var last = DateTime.MinValue;
            if (heatPumpData.Any())
            {
                var creationDates = heatPumpData.Select(hpd => hpd.DateCreated);
                foreach (var creationDate in creationDates)
                {
                    if (last < creationDate)
                    {
                        last = creationDate;
                    }
                }
            }
            else
            {
                Log.Debug("!Any() <-- GetLast!");
            }
            return DateTimeExtensions.EnsureDateTimeIsUtc(last);
        }
    }
}
