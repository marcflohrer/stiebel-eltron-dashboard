using System;
using System.Globalization;

namespace StiebelEltronApiServer.Extensions
{
    public static class DateTimeExtensions
    {
        public static int WeekOfYear(this DateTime dateTime, CultureInfo cultureInfo) 
            => cultureInfo.Calendar.GetWeekOfYear(dateTime, cultureInfo.DateTimeFormat.CalendarWeekRule, 
                                                  cultureInfo.DateTimeFormat.FirstDayOfWeek);
    }
}