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
    public class HeatPumpController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HeatPumpController(
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /HeatPump/Index
        [HttpGet]
        public IActionResult Index()
        {
            Log.Information("Entering HeatPump/Index");
            var recentSevenDays = _unitOfWork.HeatPumpStatisticsPerPeriodRepository.GetRecentSevenDays(DateTime.Now).AsEnumerable();
            if(!recentSevenDays.Any()){
                Log.Information($"Index: Recent 7 Days. No results. {recentSevenDays.Count()}");
            }else{
                Log.Information($"Index: Recent 7 Days. Results: {recentSevenDays.Count()}");
                foreach(var rsd in recentSevenDays)
                {
                    Log.Information($"Index: Recent {recentSevenDays.Count()}  {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (end) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayEnd} {rsd.PeriodKind}");
                    Log.Information($"Index: Recent {recentSevenDays.Count()}  {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (start) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayStart} ");
                    Log.Information($"Index: Recent {recentSevenDays.Count()}  {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (delta) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayDelta} ");
                    Log.Information("------------------------------");
                }
            }
            var result = recentSevenDays.ToList();

            var recent12Weeks = _unitOfWork.HeatPumpStatisticsPerPeriodRepository.GetRecentTwelveWeeks(DateTime.Now, new CultureInfo("de-DE")).AsEnumerable();
            if(!recent12Weeks.Any()){
                Log.Information($"Index: Recent 12 Weeks. No results. {recentSevenDays.Count()}");

            }else{
                Log.Information($"Index: Recent 12 Weeks. Results: {recentSevenDays.Count()}");
                foreach(var rsd in recent12Weeks)
                {
                    Log.Information($"Index: Recent {recent12Weeks.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (end) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayEnd} ");
                    Log.Information($"Index: Recent {recent12Weeks.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (start) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayStart} ");
                    Log.Information($"Index: Recent {recent12Weeks.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (delta) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayDelta} ");
                    Log.Information("------------------------------");
                }
            }
            result.AddRange(recent12Weeks);

            var recent12Months = _unitOfWork.HeatPumpStatisticsPerPeriodRepository.GetRecentTwelveMonths(DateTime.Now).AsEnumerable();
            if(!recent12Months.Any()){
                Log.Information($"Index: Recent 12 Months. No results. {recentSevenDays.Count()}");

            }else{
                Log.Information($"Index: Recent 12 Months. Results: {recentSevenDays.Count()}");
                foreach(var rsd in recent12Months)
                {
                    Log.Information($"Index: Recent {recent12Months.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (end) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayEnd} ");
                    Log.Information($"Index: Recent {recent12Months.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (start) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayStart} ");
                    Log.Information($"Index: Recent {recent12Months.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (delta) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayDelta} ");
                    Log.Information("------------------------------");
                }
            }
            result.AddRange(recent12Months);

            var recentYears = _unitOfWork.HeatPumpStatisticsPerPeriodRepository.GetYearlyRecords(DateTime.Now).AsEnumerable();
            if(!recentYears.Any()){
                Log.Information($"Index: Recent Years. No results. {recentSevenDays.Count()}");

            }else{
                Log.Information($"Index: Recent Years. Results: {recentSevenDays.Count()}");
                foreach(var rsd in recentYears)
                {
                    Log.Information($"Index: Recent {recentYears.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (end) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayEnd} ");
                    Log.Information($"Index: Recent {recentYears.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (start) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayStart} ");
                    Log.Information($"Index: Recent {recentYears.Count()} {rsd.PeriodKind}s. PowerConsumptionHotWaterDay Results: (delta) {rsd.First}; {rsd.Last}; {rsd.PowerConsumptionHotWaterDayDelta} ");
                    Log.Information("------------------------------");
                }
            }
            result.AddRange(recentYears);

            return View(result);
        }
    }
}