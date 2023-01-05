using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using StiebelEltronDashboard.Repositories;
using System;
using System.Globalization;
using System.Linq;

namespace StiebelEltronDashboard.Controllers
{
    [Authorize]
    public class MinMaxValuesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MinMaxValuesController(
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /MinMaxValues/Index
        [HttpGet]
        public IActionResult Index()
        {
            Log.Information("Entering MinMaxValues/Index");
            var recentDays = _unitOfWork.HeatPumpStatisticsPerPeriodRepository.GetRecentDays(DateTime.Now).AsEnumerable();
            if (!recentDays.Any())
            {
                Log.Debug($"Index: Recent 7 Days. No results. {recentDays.Count()}");
            }
            else
            {
                Log.Debug($"Index: Recent 7 Days. Results: {recentDays.Count()}");
                foreach (var rsd in recentDays)
                {
                    Log.Debug($"Index: Recent {recentDays.Count()}  {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (end) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayEnd} {rsd.PeriodKind}");
                    Log.Debug($"Index: Recent {recentDays.Count()}  {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (start) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayStart} ");
                    Log.Debug($"Index: Recent {recentDays.Count()}  {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (delta) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayDelta} ");
                    Log.Debug("------------------------------");
                }
            }
            var result = recentDays.ToList();

            var recentWeeks = _unitOfWork.HeatPumpStatisticsPerPeriodRepository.GetRecentWeeks(DateTime.Now, new CultureInfo("de-DE")).AsEnumerable();
            if (!recentWeeks.Any())
            {
                Log.Debug($"Index: Recent 12 Weeks. No results. {recentDays.Count()}");

            }
            else
            {
                Log.Debug($"Index: Recent 12 Weeks. Results: {recentDays.Count()}");
                foreach (var rsd in recentWeeks)
                {
                    Log.Debug($"Index: Recent {recentWeeks.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (end) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayEnd} ");
                    Log.Debug($"Index: Recent {recentWeeks.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (start) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayStart} ");
                    Log.Debug($"Index: Recent {recentWeeks.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (delta) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayDelta} ");
                    Log.Debug("------------------------------");
                }
            }
            result.AddRange(recentWeeks);

            var recentMonths = _unitOfWork.HeatPumpStatisticsPerPeriodRepository.GetRecentMonths(DateTime.Now).AsEnumerable();
            if (!recentMonths.Any())
            {
                Log.Debug($"Index: Recent 12 Months. No results. {recentDays.Count()}");

            }
            else
            {
                Log.Debug($"Index: Recent 12 Months. Results: {recentDays.Count()}");
                foreach (var rsd in recentMonths)
                {
                    Log.Debug($"Index: Recent {recentMonths.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (end) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayEnd} ");
                    Log.Debug($"Index: Recent {recentMonths.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (start) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayStart} ");
                    Log.Debug($"Index: Recent {recentMonths.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (delta) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayDelta} ");
                    Log.Debug("------------------------------");
                }
            }
            result.AddRange(recentMonths);

            var recentYears = _unitOfWork.HeatPumpStatisticsPerPeriodRepository.GetYearlyRecords(DateTime.Now).AsEnumerable();
            if (!recentYears.Any())
            {
                Log.Debug($"Index: Recent Years. No results. {recentDays.Count()}");

            }
            else
            {
                Log.Debug($"Index: Recent Years. Results: {recentDays.Count()}");
                foreach (var rsd in recentYears)
                {
                    Log.Debug($"Index: Recent {recentYears.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (end) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayEnd} ");
                    Log.Debug($"Index: Recent {recentYears.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (start) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayStart} ");
                    Log.Debug($"Index: Recent {recentYears.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (delta) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayDelta} ");
                    Log.Debug("------------------------------");
                }
            }
            result.AddRange(recentYears);

            return View(result);
        }
    }
}
