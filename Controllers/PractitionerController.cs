
using Microsoft.AspNetCore.Mvc;
using MobilePractice.Dtos;
using MobilePractice.Models;
using MobilePractice.Services;

namespace MobilePractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PractitionerController : ControllerBase {
    
    PractitionerService practitionerService;
    public PractitionerController(PractitionerService service) {
        practitionerService = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Practitioner>>> GetAllPractitioners() {
         return await practitionerService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Practitioner>> GetPractionerById() {
        throw new NotImplementedException();
    }

    [HttpPost("/login")]
    public async Task<ActionResult<PractionerDto>> LoginUser([FromBody] LoginDto loginCredentials) {
        var result = await practitionerService.Login(loginCredentials);
        if (result == null) {
            return Unauthorized();
        }
        return Ok(result);
    }

    [HttpPost("/register")]
    public async Task<ActionResult<PractionerDto>> CreateUser([FromBody] Practitioner practitioner) {
        var result = await practitionerService.RegisterUser(practitioner);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<PractionerDto>> UpdateUser([FromBody] Practitioner practitioner) {
        var result = await practitionerService.UpdateUser(practitioner);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Practitioner>> DeleteUser(long id) {
        var result = await practitionerService.DeleteUser(id);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }
}