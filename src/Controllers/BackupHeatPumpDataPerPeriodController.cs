namespace StiebelEltronDashboard.Controllers
{
    using Ionic.Zip;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StiebelEltronDashboard.Models;
    using StiebelEltronDashboard.Repositories;
    using StiebelEltronDashboard.Services;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    [AllowAnonymous]
    public class BackupHeatPumpDataPerPeriodController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public BackupHeatPumpDataPerPeriodController(
            IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            // Retrieve the data from the database
            var heatPumpDataPerPeriod = (await unitOfWork.HeatPumpStatisticsPerPeriodRepository.AllAsync()).ToList();
            var csvFilename = ZipperService.CsvFileName<HeatPumpDataPerPeriod>();
            using var memoryStream = new MemoryStream();
            using var zipFile = new ZipFile();
            var zipStream = ZipperService.ToCsvAndZip(heatPumpDataPerPeriod, memoryStream, zipFile, csvFilename);

            // Set the appropriate HTTP headers to indicate that the response should be downloaded as a file
            Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{csvFilename}\"");
            Response.ContentType = "application/zip";

            // Return the ZIP file as the response
            var zipFileName = ZipperService.ZipFileName<HeatPumpDataPerPeriod>();
            return File(zipStream.ToArray(), "application/zip", $"{zipFileName}");
        }
    }
}
