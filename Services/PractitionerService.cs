
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using MobilePractice.Data;
using MobilePractice.Models;

namespace MobilePractice.Services;
public class PractitionerService {
    private readonly PracticeContext _context;

    public PractitionerService(PracticeContext context) {
        _context = context;
    }

    public List<Practitioner> GetAll() => _context.Practitioners.ToList();

    public ActionResult<Practitioner> RegisterUser(Practitioner practitionerDto) {
        Practitioner newPractitioner = new Practitioner {
            FirstName = practitionerDto.FirstName,
            LastName = practitionerDto.LastName,
            Email = practitionerDto.Email
            // Password
        };

        return newPractitioner;
    }
}