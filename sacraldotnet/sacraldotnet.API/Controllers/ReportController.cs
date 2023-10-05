
using Microsoft.AspNetCore.Mvc;
using sacraldotnet.DTO;
using sacraldotnet.Service;
using System;
using System.Threading.Tasks;

namespace sacraldotnet.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportGenerator _reportGenerator;

        public ReportController(IReportGenerator reportGenerator)
        {
            _reportGenerator = reportGenerator;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReport(ReportRequestDto request)
        {
            try
            {
                var deliveryConfiguration = new ReportDeliveryConfiguration
                {
                    DestinationType = request.DestinationType,
                    DestinationAddress = request.DestinationAddress
                };

                if (!deliveryConfiguration.ValidateDestination())
                {
                    return BadRequest("Invalid destination address");
                }

                var fileType = GetFileType(request.FileType);

                await _reportGenerator.GenerateReport(fileType, deliveryConfiguration);

                return Ok("Report generated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private FileType GetFileType(string fileType)
        {
            return fileType switch
            {
                "PDF" => FileType.PDF,
                "CSV" => FileType.CSV,
                "Excel" => FileType.Excel,
                "Custom" => FileType.Custom,
                _ => throw new ArgumentException("Invalid file type"),
            };
        }
    }
}
