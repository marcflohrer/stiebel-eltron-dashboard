namespace StiebelEltronDashboard.Controllers
{
    using Ionic.Zip;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using StiebelEltronDashboard.Models;
    using StiebelEltronDashboard.Repositories;
    using StiebelEltronDashboard.Repositories.Models;
    using StiebelEltronDashboard.Services;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class HeatPumpDataController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public HeatPumpDataController(
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
            var zipFileName = ZipperService.ZipFileName<HeatPumpDatum>();
            Response.Headers.Append("Content-Disposition",
                                    $"attachment; filename=\"{zipFileName}s\"");
            Response.ContentType = "application/zip";

            // Return the ZIP file as the response
            return File(zipStream.ToArray(), "application/zip", zipFileName);
        }

        [HttpPost]
        public async Task<IActionResult> RestoreDatabase(IFormFile formFile)
        {
            try
            {
                var zipFile = ZipFile.Read(formFile.OpenReadStream());
                var heatputDataPerPeriodList = ZipperService.ReadDataFromZip<HeatPumpDatum, HeatPumpDatumMap>(zipFile);

                // Check if data is already stored in database
                foreach (var imported in heatputDataPerPeriodList)
                {
                    // Ensure the DateTime is in UTC
                    ToDateTimeKindUtc(imported);

                    if ((await unitOfWork.HeatPumpDataRepository.FindByDateCreated(imported.DateCreated)).Count > 1)
                    {
                        foreach (var existing in await unitOfWork.HeatPumpDataRepository.FindByDateCreated(imported.DateCreated))
                        {
                            unitOfWork.HeatPumpDataRepository.Remove(existing);
                        }
                        unitOfWork.HeatPumpDataRepository.Add(imported);
                    }
                    else if ((await unitOfWork.HeatPumpDataRepository.FindByDateCreated(imported.DateCreated)).Count != 0)
                    {
                        if (!(await unitOfWork.HeatPumpDataRepository.FindByDateCreated(imported.DateCreated)).Equals(imported))
                        {
                            unitOfWork.HeatPumpDataRepository.Update(imported);
                        }
                    }
                    else
                    {
                        unitOfWork.HeatPumpDataRepository.Add(imported);
                    }
                }

                await unitOfWork.SaveChanges();
                return Ok("Data restored successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

        private static void ToDateTimeKindUtc(HeatPumpDatum imported)
        {
            if (imported.DateCreated.Kind != DateTimeKind.Utc)
            {
                imported.DateCreated = DateTime.SpecifyKind(imported.DateCreated, DateTimeKind.Utc);
                imported.DateCreated = imported.DateCreated.ToUniversalTime();
            }
            if (imported.DateUpdated.Kind != DateTimeKind.Utc)
            {
                imported.DateUpdated = DateTime.SpecifyKind(imported.DateCreated, DateTimeKind.Utc);
                imported.DateUpdated = imported.DateCreated.ToUniversalTime();
            }
        }
    }
}
