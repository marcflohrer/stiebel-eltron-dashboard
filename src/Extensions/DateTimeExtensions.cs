using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace StiebelEltronDashboard.Extensions
{
    public static class DateTimeExtensions
    {
        public static int WeekOfYear(this DateTime dateTime, CultureInfo cultureInfo)
            => cultureInfo.Calendar.GetWeekOfYear(dateTime, cultureInfo.DateTimeFormat.CalendarWeekRule,
                                                  cultureInfo.DateTimeFormat.FirstDayOfWeek);

        public static double ToEpoch(this DateTime dateTime)
        {
            var unixReferenceDate = new DateTime(1970, 1, 1);
            TimeSpan t = dateTime - unixReferenceDate;
            Log.Debug($"ToEpoch: {dateTime.ToShortDateString()} - {unixReferenceDate.ToShortDateString()} = {t.TotalDays} d = {(double)t.TotalMilliseconds}ms");
            return (double)t.TotalMilliseconds;
        }
        public static DateTime FirstDateOfWeek(this DateTime jan1, int weekOfYear, CultureInfo cultureInfo)
        {
            int daysOffset = (int)cultureInfo.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = cultureInfo.Calendar.GetWeekOfYear(jan1, cultureInfo.DateTimeFormat.CalendarWeekRule, cultureInfo.DateTimeFormat.FirstDayOfWeek);
            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
            {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays(weekOfYear * 7);
        }
        public static DateTime GetMondayOfWeek(this DateTime dateTime, int year, int weekOfYear, CultureInfo cultureInfo)
        {
            if (weekOfYear > 53)
            {
                throw new ArgumentException($"WeekOfYear is out of range {weekOfYear}!");
            }
            var current = dateTime;
            for (int i = 0; i < 367; ++i)
            {
                if (current.DayOfWeek == DayOfWeek.Monday)
                {
                    if (current.WeekOfYear(cultureInfo) == weekOfYear)
                    {
                        return current;
                    }
                }
                current = current.AddDays(1);
            }
            return current;
        }
        public static string ToJson(this IEnumerable<DateTime> dateTimes)
            => JsonConvert.SerializeObject(dateTimes, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            });
    }
}
