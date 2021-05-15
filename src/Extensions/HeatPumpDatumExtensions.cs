using System;
using System.Collections.Generic;
using System.Linq;
using StiebelEltronDashboard.Models;

namespace StiebelEltronDashboard.Extensions {
    public static class HeatPumpDatumExtensions {
        public static double? GetMinForMetric (this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector) {
            if (!heatPumpData.Any ()) {
                Console.WriteLine("null <-- GetMinForMetric!");
                return null;
            }
            var min = Double.MaxValue;
            var metrics = heatPumpData.Select (selector);
            if (metrics.Any ()) {                
                foreach (var metric in from metric in metrics
                    where metric < min select metric) {
                    min = metric;
                }
            }
            return min;
        }

        public static double? GetMaxForMetric (this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector) {
            if (!heatPumpData.Any ()) {
                Console.WriteLine("null <-- GetAverageForMetric!");
                return null;
            }
            var max = Double.MinValue;
            var metrics = heatPumpData.Select (selector);
            if (metrics.Any ()) {
                foreach (var metric in from metric in metrics
                    where metric > max select metric) {
                    max = metric;
                }
            }
            return max;
        }

        public static double? GetAverageForMetric (this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector) {
            if (!heatPumpData.Any ()) {
                Console.WriteLine("<-- GetAverageForMetric!");
                return null;
            }
            var sum = 0.0;
            var metrics = heatPumpData.Select (selector);
            if (metrics.Any ()) {
                foreach (var metric in metrics) {
                    sum += metric;
                }
            }
            var result = sum / heatPumpData.Count ();
            return result;
        }

        public static double? GetStartForMetric (this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector) {            
            if (!heatPumpData.Any ()) {
                Console.WriteLine("null <-- GetEndForMetric!");
                return null;
            }
            var startTime = heatPumpData.Select (h => h.DateCreated).Min ();
            var startHeatPumpData = heatPumpData.Where (h => h.DateCreated == startTime);
            var startMetric = startHeatPumpData.Select (selector);
            var result = startMetric?.FirstOrDefault () ?? 0.0;
            return result;
        }

        public static double? GetEndForMetric (this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector) {
            if (!heatPumpData.Any ()) {
                Console.WriteLine("null <-- GetEndForMetric!");
                return null;
            }
            var endTime = heatPumpData.Select (h => h.DateCreated).Max ();
            var endHeatPumpData = heatPumpData.Where (h => h.DateCreated == endTime);
            var endMetric = endHeatPumpData.Select (selector);
            var result = endMetric?.FirstOrDefault () ?? 0.0;
            return result;
        }

        public static double? GetDeltaForMetric (this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector) {
            if (!heatPumpData.Any ()) {
                Console.WriteLine("null <-- GetDeltaForMetric!");
                return null;
            }
            var start = GetStartForMetric (heatPumpData, selector);
            var end = GetEndForMetric (heatPumpData, selector);
            var result = end - start;
            return result;
        }

        public static DateTime GetFirst (this IEnumerable<HeatPumpDatum> heatPumpData) {
            var first = DateTime.MaxValue;
            var creationDates = heatPumpData.Select (hpd => hpd.DateCreated);
            if(creationDates.Any()){
                foreach (var creationDate in creationDates) {
                    if (first > creationDate) {
                        first = creationDate;
                    }
                }
            }else{
                Console.WriteLine("!Any() <-- GetDeltaForMetric!");
            }
            return first;
        }

        public static DateTime GetLast (this IEnumerable<HeatPumpDatum> heatPumpData) {
            var last = DateTime.MinValue;
            if(heatPumpData.Any()){
                var creationDates = heatPumpData.Select (hpd => hpd.DateCreated);
                foreach (var creationDate in creationDates) {
                    if (last < creationDate) {
                        last = creationDate;
                    }
                }
            }else{
                Console.WriteLine("!Any() <-- GetLast!");
            }
            return last;
        }
    }
}