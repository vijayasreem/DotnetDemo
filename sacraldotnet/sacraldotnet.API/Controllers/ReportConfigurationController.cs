namespace sacraldotnet.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportConfigurationController : ControllerBase
    {
        private readonly IReportGeneratorService _reportGeneratorService;

        public ReportConfigurationController(IReportGeneratorService reportGeneratorService)
        {
            _reportGeneratorService = reportGeneratorService;
        }

        // POST api/reportconfiguration
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReportDeliveryConfiguration reportDeliveryConfiguration)
        {
            if (reportDeliveryConfiguration == null)
            {
                return BadRequest("Missing report configuration data");
            }

            try
            {
                _reportGeneratorService.ValidateDestination();
                _reportGeneratorService.ValidateDeliveryConfiguration();

                if (reportDeliveryConfiguration.DestinationType == DestinationType.Email)
                {
                    await _reportGeneratorService.GenerateReport(FileType.PDF);
                }
                else if (reportDeliveryConfiguration.DestinationType == DestinationType.CloudStorage)
                {
                    await _reportGeneratorService.GenerateReport(FileType.CSV);
                }
                else if (reportDeliveryConfiguration.DestinationType == DestinationType.InternalServer)
                {
                    await _reportGeneratorService.GenerateReport(FileType.Excel);
                }
                else if (reportDeliveryConfiguration.DestinationType == DestinationType.Custom)
                {
                    await _reportGeneratorService.GenerateReport(FileType.Custom);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}