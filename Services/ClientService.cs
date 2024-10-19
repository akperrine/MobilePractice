using MobilePractice.Data;
using MobilePractice.Models;
using Microsoft.EntityFrameworkCore;

namespace MobilePractice.Services;
public class ClientService {
    private readonly PracticeContext _context;

    public ClientService(PracticeContext context) {
        _context = context;
    }

    public async Task<List<Client>> GetAll() => await _context.Clients.ToListAsync();

    public async Task<Client?> GetTreatmentById(long id) {
        var client = await _context.Clients
                .FirstOrDefaultAsync(t => t.Id == id);

            if (client == null)
            {
                return null;
            }
            return client;
    }

    public async Task<Client?> CreateClient(Client client) {
        var existingClient = await _context.Treatments.Where(c => c.Id == client.Id).FirstOrDefaultAsync();

        if (existingClient != null) {
            return null;
        };
        DateTime timestamp = DateTime.UtcNow;
        Client newClient = client;
        newClient.StartDate = timestamp;

        await _context.AddAsync(newClient);
        await _context.SaveChangesAsync();

        return newClient;
    }

    public async Task<Client?> UpdateClient(Client client) {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();

        return client; 
    }

    public async Task<Client?> DeleteClient(long id) {
        var client = await _context.Clients.SingleAsync(t => t.Id == id);
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return client;
    }
}