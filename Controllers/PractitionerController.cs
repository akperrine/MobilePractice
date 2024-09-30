
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
    public ActionResult<List<Practitioner>> GetAllPractitioners() {
         return practitionerService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Practitioner> GetPractionerById() {
        throw new NotImplementedException();
    }

    [HttpPost("/login")]
    public ActionResult<PractionerDto> LoginUser([FromBody] LoginDto loginCredentials) {
        return practitionerService.Login(loginCredentials);
    }

    [HttpPost("/register")]
    public ActionResult<Practitioner> CreateUser([FromBody] Practitioner practitioner) {
        return practitionerService.RegisterUser(practitioner);
    }

    [HttpPut]
    public ActionResult<Practitioner> UpdateUser() {
        throw new NotImplementedException();
    }

    [HttpDelete]
    public ActionResult<Practitioner> DeleteUser() {
        throw new NotImplementedException();
    }
}