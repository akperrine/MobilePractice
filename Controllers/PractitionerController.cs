
using Microsoft.AspNetCore.Mvc;
using MobilePractice.Models;
using MobilePractice.Services;

namespace MobilePractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PractitionerController : ControllerBase {
    
    PractitionerService service;
    public PractitionerController(PractitionerService practitionerService) {
        service = practitionerService;
    }

    [HttpGet]
    public ActionResult<List<Practitioner>> GetAllPractitioners() {
         return service.GetAll();
    }
}