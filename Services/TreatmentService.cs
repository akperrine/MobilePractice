using MobilePractice.Data;
using MobilePractice.Models;
using Microsoft.EntityFrameworkCore;

namespace MobilePractice.Services;
public class TreatmentService {
    private readonly PracticeContext _context;

    public TreatmentService(PracticeContext context) {
        _context = context;
    }

    public async Task<List<Treatment>> GetAll() => await _context.Treatments.ToListAsync();

    public async Task<Treatment?> GetTreatmentById(long id) {
        var treatment = await _context.Treatments
                .FirstOrDefaultAsync(t => t.Id == id);

            if (treatment == null)
            {
                return null;
            }
            return treatment;
    }

    public async Task<Treatment?> CreateTreatment(Treatment treatment) {
        var existingTreatment = await _context.Treatments.Where(t => t.Id == treatment.Id).FirstOrDefaultAsync();

        if (existingTreatment != null) {
            return null;
        };
        DateTime timestamp = DateTime.UtcNow;
        Treatment newTreatment = treatment;

        await _context.AddAsync(newTreatment);
        await _context.SaveChangesAsync();

        return newTreatment;
    }

    public async Task<Treatment?> UpdateTreatment(Treatment treatment) {
        _context.Treatments.Update(treatment);
        await _context.SaveChangesAsync();

        return treatment; 
    }

    public async Task<Treatment?> DeleteTreatment(long id) {
        var treatment = await _context.Treatments.SingleAsync(t => t.Id == id);
        _context.Treatments.Remove(treatment);
        await _context.SaveChangesAsync();

        return treatment;
    }
}