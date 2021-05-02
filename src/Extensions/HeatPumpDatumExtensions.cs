using System;
using System.Collections.Generic;
using System.Linq;
using StiebelEltronApiServer.Models;

namespace StiebelEltronApiServer.Extensions
{
    public static class HeatPumpDatumExtensions
    {
        public static double? GetMinForMetric (this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector) {
            var min = Double.MaxValue;
            var metrics = heatPumpData.Select (selector);
            foreach (var metric in from metric in metrics
                where metric < min select metric) {
                min = metric;
            }

            return min;
        }

        public static double? GetMaxForMetric (this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector) {
            if(!heatPumpData.Any()){
                return null;
            }
            var max = Double.MinValue;
            var metrics = heatPumpData.Select (selector);
            foreach (var metric in from metric in metrics
                where metric > max select metric) {
                max = metric;
            }

            return max;
        }

        public static double? GetAverageForMetric (this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector) {
            if(!heatPumpData.Any()){
                return null;
            }
            var sum = 0.0;
            var metrics = heatPumpData.Select (selector);
            foreach (var metric in metrics) {
                sum += metric;
            }

            return sum / heatPumpData.Count ();
        }

        public static double? GetStartForMetric (this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector) {
            if(!heatPumpData.Any()){
                return null;
            }
            var startTime = heatPumpData.Select (h => h.DateCreated).Min ();
            var startHeatPumpData = heatPumpData.Where (h => h.DateCreated == startTime);
            var startMetric = startHeatPumpData.Select (selector);
            return startMetric?.FirstOrDefault () ?? 0.0;
        }

        public static double? GetEndForMetric (this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector) {
            if(!heatPumpData.Any()){
                return null;
            }
            var endTime = heatPumpData.Select (h => h.DateCreated).Max ();
            var endHeatPumpData = heatPumpData.Where (h => h.DateCreated == endTime);
            var endMetric = endHeatPumpData.Select (selector);
            return endMetric?.FirstOrDefault () ?? 0.0;
        }

        public static double? GetDeltaForMetric (this IEnumerable<HeatPumpDatum> heatPumpData, Func<HeatPumpDatum, double> selector) {
            if(!heatPumpData.Any()){
                return null;
            }
            var start = GetStartForMetric (heatPumpData, selector);
            var end = GetEndForMetric (heatPumpData, selector);
            return end - start;
        }

        public static DateTime GetFirst (this IEnumerable<HeatPumpDatum> heatPumpData) {
            var first = DateTime.MaxValue;
            var creationDates = heatPumpData.Select (hpd => hpd.DateCreated);
            foreach (var creationDate in creationDates) {
                if(first > creationDate){
                    first = creationDate;
                }
            }

            return first;
        }

        public static DateTime GetLast (this IEnumerable<HeatPumpDatum> heatPumpData) {
            var last = DateTime.MinValue;
            var creationDates = heatPumpData.Select (hpd => hpd.DateCreated);
            foreach (var creationDate in creationDates) {
                if(last < creationDate){
                    last = creationDate;
                }
            }

            return last;
        }
    }
}