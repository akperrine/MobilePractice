
using MobilePractice.Data;
using MobilePractice.Models;

namespace MobilePractice.Services;
public class PractitionerService {
    private readonly PracticeContext _context;

    public PractitionerService(PracticeContext context) {
        _context = context;
    }

    public List<Practitioner> GetAll() => _context.Practitioners.ToList();
 
}