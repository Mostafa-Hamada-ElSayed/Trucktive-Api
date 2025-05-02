using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using Trucktive.Core.Contracts.Supervisor;
using Trucktive.Core.Services;

namespace Trucktive.APIs.Controllers
{
    //[Authorize]
    public class SupervisorsController(ISupervisorService supervisorService) : ApiBaseController
    {
        private readonly ISupervisorService _supervisorService = supervisorService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupervisorRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _supervisorService.AddSupervisorAsync(request, cancellationToken);
                return Ok($"Created with Id: {result}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetSupervisorsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _supervisorService.GetSupervisorsAsync(cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupervisor([FromRoute] int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _supervisorService.GetSupervisorByIdAsync(id, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupervisor([FromRoute] int id, [FromBody] UpdateSupervisorRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _supervisorService.UpdateSupervisorAsync(id, request, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupervisor([FromRoute] int id, CancellationToken cancellationToken)
        {
            try
            {
                await _supervisorService.DeleteSupervisorAsync(id, cancellationToken);
                return Ok($"Supervisor with id {id} deleted successfully");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}