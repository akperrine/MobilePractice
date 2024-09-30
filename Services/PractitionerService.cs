
using Microsoft.AspNetCore.Mvc;
using BCrypt;
using MobilePractice.Data;
using MobilePractice.Models;
using BCrypt.Net;
using MobilePractice.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

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

    public async Task<PractionerDto?> Login(LoginDto credentials) {
        var practioner = await _context.Practitioners.SingleAsync(prac => prac.Email == credentials.Email);
        if (practioner == null) {
            return null; //figure out how to do this
        };
        bool verifiedPassword = BCrypt.Net.BCrypt.Verify(credentials.Password, practioner.Password);
        if (!verifiedPassword) {
            return null;
        }
        PractionerDto practionerDto = new PractionerDto {
            Id = practioner.Id,
            FirstName = practioner.FirstName,
            LastName = practioner.LastName,
            Email = practioner.Email,
            StartDate = practioner.StartDate,
            Earnings = practioner.Earnings,
            Address = practioner.Address,
            Latitude = practioner.Latitude,
            Longitude = practioner.Longitude,
        };
        return practionerDto;
    }
}