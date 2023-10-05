Controller :

using sacraldotnet.API;
using sacraldotnet.DTO;
using sacraldotnet.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sacraldotnet.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportGeneratorController : ControllerBase
    {
        private readonly IReportGeneratorRepository _reportGeneratorRepository;

        public ReportGeneratorController(IReportGeneratorRepository reportGeneratorRepository)
        {
            _reportGeneratorRepository = reportGeneratorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportDeliveryConfigurationModel>>> GetReportDeliveryConfigurations()
        {
            return Ok(await _reportGeneratorRepository.GetReportDeliveryConfigurationsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReportDeliveryConfigurationModel>> GetReportDeliveryConfiguration(int id)
        {
            var configuration = await _reportGeneratorRepository.GetReportDeliveryConfigurationAsync(id);

            if (configuration == null)
            {
                return NotFound();
            }

            return Ok(configuration);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateReportDeliveryConfiguration(ReportDeliveryConfigurationModel config)
        {
            await _reportGeneratorRepository.ValidateDestinationAsync(config);
            await _reportGeneratorRepository.ValidateDeliveryConfigurationAsync(config);

            var id = await _reportGeneratorRepository.CreateReportDeliveryConfigurationAsync(config);
            return CreatedAtAction(nameof(GetReportDeliveryConfiguration), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReportDeliveryConfiguration(int id, ReportDeliveryConfigurationModel config)
        {
            await _reportGeneratorRepository.ValidateDestinationAsync(config);
            await _reportGeneratorRepository.ValidateDeliveryConfigurationAsync(config);

            await _reportGeneratorRepository.UpdateReportDeliveryConfigurationAsync(config);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportDeliveryConfiguration(int id)
        {
            await _reportGeneratorRepository.DeleteReportDeliveryConfigurationAsync(id);
            return NoContent();
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateReport(FileType fileType, ReportDeliveryConfigurationModel config)
        {
            await _reportGeneratorRepository.GenerateReportAsync(fileType, config);
            return Ok();
        }

        [HttpPost("deliver")]
        public async Task<IActionResult> DeliverReport(ReportDeliveryConfigurationModel config)
        {
            await _reportGeneratorRepository.DeliverReportAsync(config);
            return Ok();
        }
    }
}