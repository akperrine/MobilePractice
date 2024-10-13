
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobilePractice.Dtos;
using MobilePractice.Models;
using MobilePractice.Services;

namespace MobilePractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PractitionerController : ControllerBase {
    
    JwtService jwtService;

    PractitionerService practitionerService;
    public PractitionerController(PractitionerService service, JwtService aJwtService) {
        practitionerService = service;
        jwtService = aJwtService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Practitioner>>> GetAllPractitioners() {
         return await practitionerService.GetAll();
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<PractitionerDto>> GetPractitionerById(long id) {
        var result = await practitionerService.GetPractitionerById(id);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPost("/login")]
    public async Task<ActionResult<PractitionerDto>> LoginPractitioner([FromBody] LoginDto loginCredentials) {
        var result = await practitionerService.Login(loginCredentials);
        if (result == null) {
            return Unauthorized();
        }
        var token = jwtService.GenerateToken(result);
        return Ok(new {token = token});
    }

    [HttpPost("/register")]
    public async Task<ActionResult<PractitionerDto>> CreatePractitioner([FromBody] Practitioner practitioner) {
        var result = await practitionerService.RegisterPractitioner(practitioner);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<PractitionerDto>> UpdatePractitioner([FromBody] Practitioner practitioner) {
        var result = await practitionerService.UpdatePractitioner(practitioner);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Practitioner>> DeletePractitioner(long id) {
        var result = await practitionerService.DeletePractitioner(id);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }
}