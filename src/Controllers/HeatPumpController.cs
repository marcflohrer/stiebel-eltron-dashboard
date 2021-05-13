using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StiebelEltronApiServer.Repositories;
using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace StiebelEltronApiServer.Controllers
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
            Console.WriteLine("Entering HeatPump/Index");
            var recentSevenDays = _unitOfWork.HeatPumpStatisticsPerPeriodRepository.GetRecentSevenDays(DateTime.Now).AsEnumerable();
            if(!recentSevenDays.Any()){
                Console.WriteLine($"Index: Recent 7 Days. No results. {recentSevenDays.Count()}");
            }else{
                Console.WriteLine($"Index: Recent 7 Days. Results: {recentSevenDays.Count()}");
                foreach(var rsd in recentSevenDays)
                {
                    Console.WriteLine($"Index: Recent 7 Days. Results: (max) {rsd.DateCreated}; {rsd.First}; {rsd.OutdoorTemperatureMax} {rsd.PeriodKind}");
                    Console.WriteLine($"Index: Recent 7 Days. Results: (min) {rsd.DateCreated}; {rsd.First}; {rsd.OutdoorTemperatureMin} ");
                    Console.WriteLine($"Index: Recent 7 Days. Results: (avg) {rsd.DateCreated}; {rsd.First}; {rsd.OutdoorTemperatureAverage} ");
                    Console.WriteLine("------------------------------");
                }
            }
            var result = recentSevenDays.ToList();

            var recent12Weeks = _unitOfWork.HeatPumpStatisticsPerPeriodRepository.GetRecentTwelveWeeks(DateTime.Now, new CultureInfo("de-DE")).AsEnumerable();
            if(!recent12Weeks.Any()){
                Console.WriteLine($"Index: Recent 12 Weeks. No results. {recentSevenDays.Count()}");

            }else{
                Console.WriteLine($"Index: Recent 12 Weeks. Results: {recentSevenDays.Count()}");
                foreach(var rsd in recentSevenDays)
                {
                    Console.WriteLine($"Index: Recent 12 Weeks. Results: (max) {rsd.DateCreated}; {rsd.First}; {rsd.OutdoorTemperatureMax} {rsd.PeriodKind}");
                    Console.WriteLine($"Index: Recent 12 Weeks. Results: (min) {rsd.DateCreated}; {rsd.First}; {rsd.OutdoorTemperatureMin} ");
                    Console.WriteLine($"Index: Recent 12 Weeks. Results: (avg) {rsd.DateCreated}; {rsd.First}; {rsd.OutdoorTemperatureAverage} ");
                    Console.WriteLine("------------------------------");
                }
            }
            result.AddRange(recent12Weeks);

            var recent12Months = _unitOfWork.HeatPumpStatisticsPerPeriodRepository.GetRecentTwelveMonths(DateTime.Now).AsEnumerable();
            if(!recent12Months.Any()){
                Console.WriteLine($"Index: Recent 12 Months. No results. {recentSevenDays.Count()}");

            }else{
                Console.WriteLine($"Index: Recent 12 Months. Results: {recentSevenDays.Count()}");
                foreach(var rsd in recentSevenDays)
                {
                    Console.WriteLine($"Index: Recent 12 Months. Results: (max) {rsd.DateCreated}; {rsd.First}; {rsd.OutdoorTemperatureMax} {rsd.PeriodKind}");
                    Console.WriteLine($"Index: Recent 12 Months. Results: (min) {rsd.DateCreated}; {rsd.First}; {rsd.OutdoorTemperatureMin} ");
                    Console.WriteLine($"Index: Recent 12 Months. Results: (avg) {rsd.DateCreated}; {rsd.First}; {rsd.OutdoorTemperatureAverage} ");
                    Console.WriteLine("------------------------------");
                }
            }
            result.AddRange(recent12Months);

            var recentYears = _unitOfWork.HeatPumpStatisticsPerPeriodRepository.GetYearlyRecords(DateTime.Now).AsEnumerable();
            if(!recentYears.Any()){
                Console.WriteLine($"Index: Recent Years. No results. {recentSevenDays.Count()}");

            }else{
                Console.WriteLine($"Index: Recent Years. Results: {recentSevenDays.Count()}");
                foreach(var rsd in recentSevenDays)
                {
                    Console.WriteLine($"Index: Recent Years. Results: (max) {rsd.DateCreated}; {rsd.First}; {rsd.OutdoorTemperatureMax} {rsd.PeriodKind}");
                    Console.WriteLine($"Index: Recent Years. Results: (min) {rsd.DateCreated}; {rsd.First}; {rsd.OutdoorTemperatureMin} ");
                    Console.WriteLine($"Index: Recent Years. Results: (avg) {rsd.DateCreated}; {rsd.First}; {rsd.OutdoorTemperatureAverage} ");
                    Console.WriteLine("------------------------------");
                }
            }
            result.AddRange(recentYears);

            return View(result);
        }
    }
}