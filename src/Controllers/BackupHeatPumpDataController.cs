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
    public class BackupHeatPumpDataController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public BackupHeatPumpDataController(
            IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            // Retrieve the data from the database
            var heatPumpData = (await unitOfWork.HeatPumpDataRepository.AllAsync()).ToList();
            // Create a ZIP file and add the CSV file to it using DotNetZip
            var csvFilename = CsvService.CsvFileName<HeatPumpDatum>();
            using var memoryStream = new MemoryStream();
            using var zipFile = new ZipFile();
            var cvsString = CsvService.ToCsvString(heatPumpData);
            var zipStream = ZipperService.Zip(cvsString, memoryStream, zipFile, csvFilename);

            // Set the appropriate HTTP headers to indicate that the response should be downloaded as a file
            var zipFileName = ZipperService.ZipFileName<HeatPumpDataPerPeriod>();
            Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{zipFileName}s\"");
            Response.ContentType = "application/zip";

            // Return the ZIP file as the response
            return File(zipStream.ToArray(), "application/zip", zipFileName);
        }
    }
}
