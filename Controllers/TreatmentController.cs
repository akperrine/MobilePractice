
using Microsoft.AspNetCore.Mvc;
using MobilePractice.Models;
using MobilePractice.Services;

namespace MobilePractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TreatmentController : ControllerBase {
    
    TreatmentService treatmentService;
    public TreatmentController(TreatmentService service) {
        treatmentService = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Treatment>>> GetAllTreatments() {
         return await treatmentService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Treatment>> GetTreatmentById(long id) {
        var result = await treatmentService.GetTreatmentById(id);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<ActionResult<Treatment>> CreateTreatment([FromBody] Treatment treatment) {
        var result = await treatmentService.CreateTreatment(treatment);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<Treatment>> UpdateTreatment([FromBody] Treatment treatment) {
        var result = await treatmentService.UpdateTreatment(treatment);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Treatment>> DeleteTreatment(long id) {
        var result = await treatmentService.DeleteTreatment(id);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }
}