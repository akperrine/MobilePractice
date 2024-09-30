
using Microsoft.AspNetCore.Mvc;
using BCrypt;
using MobilePractice.Data;
using MobilePractice.Models;
using BCrypt.Net;
using MobilePractice.Dtos;

namespace MobilePractice.Services;
public class PractitionerService {
    private readonly PracticeContext _context;

    public PractitionerService(PracticeContext context) {
        _context = context;
    }

    public List<Practitioner> GetAll() => _context.Practitioners.ToList();

    public ActionResult<Practitioner> RegisterUser(Practitioner practitionerDto) {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(practitionerDto.Password);
        DateTime timestamp = DateTime.UtcNow;
        Practitioner newPractitioner = new Practitioner {
            FirstName = practitionerDto.FirstName,
            LastName = practitionerDto.LastName,
            Email = practitionerDto.Email,
            Password = hashedPassword,
            StartDate = timestamp
        };

        _context.Add(newPractitioner);
        _context.SaveChanges();

        return newPractitioner;
    }

    public ActionResult<PractionerDto> Login(LoginDto credentials) {
        PractionerDto practioner = new PractionerDto {};
        return practioner;
    }
}