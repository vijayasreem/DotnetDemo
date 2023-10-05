
using Microsoft.AspNetCore.Mvc;
using sacraldotnet.DTO;
using sacraldotnet.Service;

namespace sacraldotnet.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportGenerator _reportGenerator;
        private readonly IReportScheduler _reportScheduler;
        private readonly IReportDeliveryConfigurationValidation _deliveryConfigValidation;

        public ReportController(IReportGenerator reportGenerator, IReportScheduler reportScheduler, IReportDeliveryConfigurationValidation deliveryConfigValidation)
        {
            _reportGenerator = reportGenerator;
            _reportScheduler = reportScheduler;
            _deliveryConfigValidation = deliveryConfigValidation;
        }

        [HttpPost("GenerateReport")]
        public async Task<IActionResult> GenerateReport([FromBody] ReportRequestDTO request)
        {
            if (!_deliveryConfigValidation.ValidateDestination(request.DeliveryConfiguration))
            {
                return BadRequest("Invalid delivery configuration");
            }

            try
            {
                await _reportGenerator.GenerateReport(request.FileType, request.DeliveryConfiguration);

                return Ok("Report generated successfully");
            }
            catch (Exception ex)
            {
                // Handle exception and return appropriate error response
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ScheduleReports")]
        public async Task<IActionResult> ScheduleReports()
        {
            try
            {
                await _reportScheduler.ScheduleReports();

                return Ok("Reports scheduled successfully");
            }
            catch (Exception ex)
            {
                // Handle exception and return appropriate error response
                return StatusCode(500, ex.Message);
            }
        }
    }
}
