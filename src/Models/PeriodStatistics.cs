using System;

namespace StiebelEltronDashboard.Models
{
    public record PeriodStatistics(int Year, int PeriodNumber, PeriodKind PeriodKind, DateTime Start, DateTime End, HeatPumpDataPerPeriod HeatPumpDataPerPeriod);
}