using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public static DateTime GetMondayOfWeek (this DateTime dateTime, int year, int weekOfYear, CultureInfo cultureInfo) 
        {
            if(weekOfYear > 53){
                throw new ArgumentException($"WeekOfYear is out of range {weekOfYear}!");
            }
            var current = dateTime;
            for(int i = 0; i < 367; ++i){
                if(current.DayOfWeek == DayOfWeek.Monday){
                    if(current.WeekOfYear(cultureInfo) == weekOfYear){
                        return current;
                    }
                }
                current = current.AddDays(1);
            }
            return current; 
        }
        public static string ToJson (this IEnumerable<DateTime> dateTimes) 
            => JsonConvert.SerializeObject(dateTimes);                                  
    }
}