
using Microsoft.AspNetCore.Mvc;
using MobilePractice.Models;

namespace MobilePractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PractitionerController : ControllerBase {
    public PractitionerController() {}

    [HttpGet]
    public ActionResult<List<Practitioner>> GetAllPractitioners() {
         throw new NotImplementedException();
    }
}