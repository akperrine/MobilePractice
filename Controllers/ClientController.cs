using Microsoft.AspNetCore.Mvc;
using MobilePractice.Models;
using MobilePractice.Services;

namespace MobilePractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase {
    
    ClientService clientService;
    public ClientController(ClientService service) {
        clientService = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Client>>> GetAllTreatments() {
         return await clientService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClientById(long id) {
        var result = await clientService.GetClientById(id);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPost("/create")]
    public async Task<ActionResult<Client>> CreateClient([FromBody] Client client) {
        var result = await clientService.CreateClient(client);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<Client>> UpdateClient([FromBody] Client client) {
        var result = await clientService.UpdateClient(client);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Client>> DeleteClient(long id) {
        var result = await clientService.DeleteClient(id);
        if (result == null) {
            return BadRequest();
        }
        return Ok(result);
    }
}