
using Microsoft.AspNetCore.Mvc;
using sacraldotnet.DTO;
using sacraldotnet.Service;
using System.Threading.Tasks;

namespace sacraldotnet.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportConfigurationController : ControllerBase
    {
        private readonly IReportConfigurationService _reportConfigurationService;

        public ReportConfigurationController(IReportConfigurationService reportConfigurationService)
        {
            _reportConfigurationService = reportConfigurationService;
        }

        [HttpPost]
        public async Task<IActionResult> ConfigureReport([FromBody] ReportConfigurationDto reportConfiguration)
        {
            try
            {
                await _reportConfigurationService.ConfigureReport(reportConfiguration.FileType, reportConfiguration.Destination, reportConfiguration.Frequency);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
