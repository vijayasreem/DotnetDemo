
using Microsoft.AspNetCore.Mvc;
using sacraldotnet.DTO;
using sacraldotnet.Service;

namespace sacraldotnet.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportConfigurationController : ControllerBase
    {
        private readonly IReportGenerator _reportGenerator;
        private readonly ISchedulerService _schedulerService;

        public ReportConfigurationController(IReportGenerator reportGenerator, ISchedulerService schedulerService)
        {
            _reportGenerator = reportGenerator;
            _schedulerService = schedulerService;
        }

        [HttpPost]
        public async Task<IActionResult> ConfigureReportDelivery(ReportDeliveryConfigurationDto configurationDto)
        {
            // Validate the report delivery configuration
            if (!configurationDto.ValidateDestination() || !configurationDto.ValidateDeliveryConfiguration())
            {
                return BadRequest("Invalid report delivery configuration.");
            }

            // Generate the report based on the selected file type
            await _reportGenerator.GenerateReport(configurationDto.FileType);

            // Schedule the report generation
            await _schedulerService.ScheduleReportGeneration();

            return Ok("Report delivery configuration saved successfully.");
        }
    }
}
