using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trucktive.Core.Contracts.Drivers;
using Trucktive.Core.Services;

namespace Trucktive.APIs.Controllers
{
    //[Authorize]
    public class DriversController(IDriverService driverService) : ApiBaseController
    {
        private readonly IDriverService _driverService = driverService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDriverRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _driverService.AddDriverAsync(request, cancellationToken);

                return Ok($"Create with Id: {result}");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetDriversAsync(CancellationToken cancellationToken)
        {
            var result = await _driverService.GetDriversAsync(cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDriver([FromRoute] int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _driverService.GetDriverByIdAsync(id, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDriver([FromRoute] int id, [FromBody] UpdateDriverRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _driverService.UpdateDriverAsync(id, request, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver([FromRoute] int id, CancellationToken cancellationToken)
        {
            try
            {
                await _driverService.DeleteDriverAsync(id, cancellationToken);
                return Ok($"Driver with id {id} Deleted Successfully");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
