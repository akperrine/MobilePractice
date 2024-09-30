
using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("/login")]
    public ActionResult<Practitioner> LoginUser() {
        throw new NotImplementedException();
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