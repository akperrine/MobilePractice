
using Microsoft.AspNetCore.Mvc;
using MobilePractice.Models;
using MobilePractice.Services;

namespace MobilePractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase {
    
    AppointmentService AppointmentService;
    public AppointmentController(AppointmentService service) {
        AppointmentService = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Appointment>>> GetAllAppointments() {
         return await AppointmentService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Appointment>> GetAppointmentById(long id) {
        var result = await AppointmentService.GetAppointmentById(id);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPost("/create")]
    public async Task<ActionResult<Appointment>> CreateAppointment([FromBody] Appointment appointment) {
        var result = await AppointmentService.CreateAppointment(appointment);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<Appointment>> UpdateAppointment([FromBody] Appointment appointment) {
        var result = await AppointmentService.UpdateAppointment(appointment);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Appointment>> DeleteAppointment(long id) {
        var result = await AppointmentService.DeleteAppointment(id);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }
}