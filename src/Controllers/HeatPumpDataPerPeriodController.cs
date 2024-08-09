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
    public class HeatPumpDataPerPeriodController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public HeatPumpDataPerPeriodController(
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
            // Create a ZIP file and add the CSV file to it using DotNetZip
            var csvFilename = CsvService.CsvFileName<HeatPumpDataPerPeriod>();
            using var memoryStream = new MemoryStream();
            using var zipFile = new ZipFile();
            var cvsString = CsvService.ToCsvString(heatPumpDataPerPeriod);
            var zipStream = ZipperService.Zip(cvsString, memoryStream, zipFile, csvFilename);

            // Set the appropriate HTTP headers to indicate that the response should be downloaded as a file
            var zipFileName = ZipperService.ZipFileName<HeatPumpDataPerPeriod>();
            Response.Headers.ContentDisposition = $"attachment; filename=\"{zipFileName}\"";
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
                var heatputDataPerPeriodList = ZipperService.ReadDataFromZip<HeatPumpDataPerPeriod, HeatPumpDataPerPeriodMap>(zipFile);

                // Check if data is already stored in database
                foreach (var imported in heatputDataPerPeriodList)
                {
                    ToDateTimeKindUtc(imported);

                    var existingList = await unitOfWork.HeatPumpStatisticsPerPeriodRepository.FindByPeriodAsync(
                        imported.Year,
                        imported.PeriodKind,
                        imported.PeriodNumber);
                    if (existingList.Count > 1)
                    {
                        foreach (var existing in existingList)
                        {
                            unitOfWork.HeatPumpStatisticsPerPeriodRepository.Remove(existing);
                        }
                        unitOfWork.HeatPumpStatisticsPerPeriodRepository.Add(imported);
                    }
                    else if (existingList.Any())
                    {
                        if (!existingList.Equals(imported))
                        {
                            unitOfWork.HeatPumpStatisticsPerPeriodRepository.Update(imported);
                        }
                    }
                    else
                    {
                        unitOfWork.HeatPumpStatisticsPerPeriodRepository.Add(imported);
                    }
                }

                await unitOfWork.SaveChanges();
                return Ok("Data restored successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Bad Request: {ex.Message} - {ex.InnerException?.Message}");
            }
        }    

        private static DateTime EnsureDateTimeIsUtc(DateTime dateTime)
        {
            if (dateTime.Kind != DateTimeKind.Utc)
            {
                dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                dateTime = dateTime.ToUniversalTime();
            }
            return dateTime;
        }

        private static void ToDateTimeKindUtc(HeatPumpDataPerPeriod imported)
        {
            imported.DateCreated = EnsureDateTimeIsUtc(imported.DateCreated);
            imported.DateUpdated = EnsureDateTimeIsUtc(imported.DateUpdated);
            imported.PeriodStart = EnsureDateTimeIsUtc(imported.PeriodStart);
            imported.PeriodEnd = EnsureDateTimeIsUtc(imported.PeriodEnd);
            imported.First = EnsureDateTimeIsUtc(imported.First);
            imported.Last = EnsureDateTimeIsUtc(imported.Last);
        }
    }
}
