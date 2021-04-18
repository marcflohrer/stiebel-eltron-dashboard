using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StiebelEltronApiServer.Models;
using StiebelEltronApiServer.Models.HeatPumpViewModels;
using StiebelEltronApiServer.Repositories;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            Console.WriteLine("Entering HeatPump/Index");
            var maxTotalPowerConsumption = await _unitOfWork.HeatPumpDataRepository.GetMaxTotalPowerConsumption();
            Console.WriteLine($"Index: Max Total Power Consumption: {maxTotalPowerConsumption}");
            var model = new HeatPumpDataViewModel
            {
                MaxTotalPowerConsumption = maxTotalPowerConsumption.TotalPowerConsumption
            };
            return View(model);
        }
    }
}