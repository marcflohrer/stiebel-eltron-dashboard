using System;
using System.Globalization;

namespace StiebelEltronApiServer.Extensions
{
    public static class DateTimeExtensions
    {
        public static int WeekOfYear(this DateTime dateTime, CultureInfo cultureInfo) 
            => cultureInfo.Calendar.GetWeekOfYear(dateTime, cultureInfo.DateTimeFormat.CalendarWeekRule, 
                                                  cultureInfo.DateTimeFormat.FirstDayOfWeek);

        public static double ToEpoch(this DateTime dateTime){
            var unixReferenceDate = new DateTime(1970, 1, 1);
            TimeSpan t = dateTime - unixReferenceDate;
            Console.WriteLine($"ToEpoch: {dateTime.ToShortDateString()} - {unixReferenceDate.ToShortDateString()} = {t.TotalDays} d = {(double)t.TotalMilliseconds}ms");
            return (double)t.TotalMilliseconds;
        }                                                  
    }
}