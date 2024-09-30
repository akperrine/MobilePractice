
using Microsoft.AspNetCore.Mvc;
using BCrypt;
using MobilePractice.Data;
using MobilePractice.Models;
using BCrypt.Net;
using MobilePractice.Dtos;
using Microsoft.EntityFrameworkCore;

namespace MobilePractice.Services;
public class PractitionerService {
    private readonly PracticeContext _context;

    public PractitionerService(PracticeContext context) {
        _context = context;
    }

    public async Task<ActionResult<List<Practitioner>>> GetAll() => await _context.Practitioners.ToListAsync();

    public async Task<ActionResult<Practitioner>> RegisterUser(Practitioner practitionerDto) {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(practitionerDto.Password);
        DateTime timestamp = DateTime.UtcNow;
        Practitioner newPractitioner = new Practitioner {
            FirstName = practitionerDto.FirstName,
            LastName = practitionerDto.LastName,
            Email = practitionerDto.Email,
            Password = hashedPassword,
            StartDate = timestamp
        };

        await _context.AddAsync(newPractitioner);
        await _context.SaveChangesAsync();

        return newPractitioner;
    }

    public async Task<ActionResult<PractionerDto>> Login(LoginDto credentials) {
        PractionerDto practioner = new PractionerDto {};
        return practioner;
    }
}