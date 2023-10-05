
using Microsoft.AspNetCore.Mvc;
using sacraldotnet.DTO;
using sacraldotnet.Service;
using System;
using System.Threading.Tasks;

namespace sacraldotnet.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportDeliveryConfigurationController : ControllerBase
    {
        private readonly IReportDeliveryConfigurationService _reportDeliveryConfigurationService;

        public ReportDeliveryConfigurationController(IReportDeliveryConfigurationService reportDeliveryConfigurationService)
        {
            _reportDeliveryConfigurationService = reportDeliveryConfigurationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ReportDeliveryConfigurationModel model)
        {
            try
            {
                int id = await _reportDeliveryConfigurationService.CreateAsync(model);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var reportDeliveryConfiguration = await _reportDeliveryConfigurationService.GetByIdAsync(id);
                if (reportDeliveryConfiguration == null)
                {
                    return NotFound();
                }
                return Ok(reportDeliveryConfiguration);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var reportDeliveryConfigurations = await _reportDeliveryConfigurationService.GetAllAsync();
                return Ok(reportDeliveryConfigurations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ReportDeliveryConfigurationModel model)
        {
            try
            {
                var existingReportDeliveryConfiguration = await _reportDeliveryConfigurationService.GetByIdAsync(id);
                if (existingReportDeliveryConfiguration == null)
                {
                    return NotFound();
                }
                await _reportDeliveryConfigurationService.UpdateAsync(model);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var existingReportDeliveryConfiguration = await _reportDeliveryConfigurationService.GetByIdAsync(id);
                if (existingReportDeliveryConfiguration == null)
                {
                    return NotFound();
                }
                await _reportDeliveryConfigurationService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
