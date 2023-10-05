namespace sacraldotnet.API
{
    using sacraldotnet.DTO;
    using sacraldotnet.Service;
    using Microsoft.AspNetCore.Mvc;
 
    [Route("api/[controller]")]
    [ApiController]
    public class ReportGeneratorController : ControllerBase
    {
        private readonly IReportGeneratorService reportGeneratorService;

        public ReportGeneratorController(IReportGeneratorService reportGeneratorService)
        {
            this.reportGeneratorService = reportGeneratorService;
        }
 
        // POST: api/ReportGenerator
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReportDeliveryConfiguration config)
        {
            try
            {
                await reportGeneratorService.GenerateReportAsync(config);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}