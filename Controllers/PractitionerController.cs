
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
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
    public async Task<ActionResult<LoginResponseDto>> LoginPractitioner([FromBody] LoginDto loginCredentials) {
        var result = await practitionerService.Login(loginCredentials);
        if (result == null) {
            return Unauthorized();
        }
        var token = jwtService.GenerateToken(result);
          var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginCredentials.Email)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
 
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        return Ok(new LoginResponseDto{
            Data = result,
            Token = token
        });
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