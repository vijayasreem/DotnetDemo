
using Microsoft.AspNetCore.Mvc;
using sacraldotnet.DTO;
using sacraldotnet.Service;

namespace sacraldotnet.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportGenerator _reportGenerator;
        private readonly IReportDeliveryConfiguration _reportDeliveryConfiguration;

        public ReportController(IReportGenerator reportGenerator, IReportDeliveryConfiguration reportDeliveryConfiguration)
        {
            _reportGenerator = reportGenerator;
            _reportDeliveryConfiguration = reportDeliveryConfiguration;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReport([FromBody] FileType fileType)
        {
            try
            {
                await _reportGenerator.GenerateReport(fileType);

                // Return success message
                return Ok("Report generated successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception
                // Return error message
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while generating the report.");
            }
        }

        [HttpPost("delivery-configuration")]
        public IActionResult ValidateDeliveryConfiguration([FromBody] ReportDeliveryConfiguration deliveryConfiguration)
        {
            if (!deliveryConfiguration.ValidateDestination())
            {
                return BadRequest("Invalid destination address.");
            }

            if (!deliveryConfiguration.ValidateDeliveryConfiguration())
            {
                return BadRequest("Invalid delivery configuration.");
            }

            // Return success message
            return Ok("Delivery configuration validated successfully.");
        }
    }
}
