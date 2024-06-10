using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registration>>> GetRegistrations()
        {
            return Ok(await _registrationService.GetRegistrationsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Registration>> GetRegistration(int id)
        {
            var registration = await _registrationService.GetRegistrationByIdAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            return Ok(registration);
        }

        [HttpPost]
        public async Task<ActionResult> AddRegistration(Registration registration)
        {
            await _registrationService.AddRegistrationAsync(registration);
            return CreatedAtAction(nameof(GetRegistration), new { id = registration.Id }, registration);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegistration(int id, Registration registration)
        {
            if (id != registration.Id)
            {
                return BadRequest();
            }
            await _registrationService.UpdateRegistrationAsync(registration);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistration(int id)
        {
            await _registrationService.DeleteRegistrationAsync(id);
            return NoContent();
        }
    }
}