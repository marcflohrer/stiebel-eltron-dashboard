using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StiebelEltronApiServer.Extensions;
using StiebelEltronApiServer.Models.HeatPumpViewModels;
using StiebelEltronApiServer.Repositories;
using System;
using System.Collections.Generic;
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
            var recentSevenDays = _unitOfWork.HeatPumpStatisticsPerPeriodRepository.GetRecentSevenDays(DateTime.Now);
            if(!recentSevenDays.Any()){
                Console.WriteLine($"Index: Recent 7 Days. No results. {recentSevenDays.Count()}");
                return View();
            }else{
                Console.WriteLine($"Index: Recent 7 Days. Results: {recentSevenDays.Count()}");
                foreach(var rsd in recentSevenDays)
                {
                    Console.WriteLine($"Index: Recent 7 Days. Results: {rsd.DateCreated}; {rsd.First}; {rsd.OutdoorTemperatureMax} ");
                }
            }
            return View(recentSevenDays);
        }
    }

    [DataContract]
	public class DataPoint
	{
		public DataPoint(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}
 
		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "x")]
		public Nullable<double> X = null;
 
		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "y")]
		public Nullable<double> Y = null;
	}
}