﻿namespace StiebelEltronDashboard.Services
{
    using StiebelEltronDashboard.Extensions;
    using StiebelEltronDashboard.Models;
    using System;
    using System.Globalization;

    public static class PeriodDateProvider
    {
        public static DateTime GetPeriodStart(int year, PeriodKind periodKind, int periodNumber) => periodKind switch
        {
            PeriodKind.Day => DateTimeExtensions.EnsureDateTimeIsUtc(new DateTime(year, 1, 1).AddDays(periodNumber - 1)),
            PeriodKind.Week => DateTimeExtensions.EnsureDateTimeIsUtc(FirstDateOfWeek(year, periodNumber, new CultureInfo("de-DE"))),
            PeriodKind.Month => DateTimeExtensions.EnsureDateTimeIsUtc(new DateTime(year, periodNumber, 1)),
            PeriodKind.Year => DateTimeExtensions.EnsureDateTimeIsUtc(new DateTime(year, 1, 1)),
            _ => throw new ArgumentOutOfRangeException(nameof(periodKind), $"Not expected periodKind value: {periodKind}"),
        };

        public static DateTime GetPeriodEnd(int year, PeriodKind periodKind, int periodNumber)
        {
            var periodStart = GetPeriodStart(year, periodKind, periodNumber);
            var nextMonth = DateTime.MinValue;
            if (periodKind == PeriodKind.Month)
            {
                nextMonth = new DateTime(year, periodNumber, 1).AddDays(31);
            }
            var result = periodStart.Add(periodKind switch
            {
                PeriodKind.Day => TimeSpan.FromDays(1),
                PeriodKind.Week => TimeSpan.FromDays(7),
                PeriodKind.Month => new DateTime(nextMonth.Year, nextMonth.Month, 1).Subtract(periodStart),
                PeriodKind.Year => new DateTime(year + 1, 1, 1).Subtract(periodStart),
                _ => throw new ArgumentOutOfRangeException(nameof(periodKind), $"Not expected periodKind value: {periodKind}"),
            });
            return DateTimeExtensions.EnsureDateTimeIsUtc(result);
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear, CultureInfo ci)
        {
            // What's the first day of the reference week
            var firstDayOfReferenceWeek = new DateTime(year, 7, 1);
            while (firstDayOfReferenceWeek.DayOfWeek != ci.DateTimeFormat.FirstDayOfWeek)
            {
                firstDayOfReferenceWeek = firstDayOfReferenceWeek.AddDays(1.0);
            }
            // What's the reference week number?
            var refWeek = ci.Calendar.GetWeekOfYear(firstDayOfReferenceWeek, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);

            // What's the offset in weeks from the reference week?
            var weekOffset = weekOfYear - refWeek;

            // Offset times seven is the offset in days to the first day of the reference week
            return DateTimeExtensions.EnsureDateTimeIsUtc(firstDayOfReferenceWeek.AddDays(7 * weekOffset));
        }
    }
}
