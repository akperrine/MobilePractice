
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

    public async Task<List<Practitioner>> GetAll() => await _context.Practitioners.ToListAsync();

    public async Task<PractionerDto?> RegisterUser(Practitioner practitionerDto) {
        var existingPractioner = await _context.Practitioners.Where(prac => prac.Email == practitionerDto.Email).FirstOrDefaultAsync();

        if (existingPractioner != null) {
            return null;
        };

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

        PractionerDto newPractitionerDto = new PractionerDto {
            Id = newPractitioner.Id,
            FirstName = newPractitioner.FirstName,
            LastName = newPractitioner.LastName,
            Email = newPractitioner.Email,
            StartDate = newPractitioner.StartDate,
            Earnings = newPractitioner.Earnings,
            Address = newPractitioner.Address,
            Latitude = newPractitioner.Latitude,
            Longitude = newPractitioner.Longitude,
        };

        return newPractitionerDto;
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

    public async Task<PractionerDto?> UpdateUser(Practitioner practionerData) {
        _context.Practitioners.Update(practionerData);
        await _context.SaveChangesAsync();

        PractionerDto practionerDto = new PractionerDto {
            Id = practionerData.Id,
            FirstName = practionerData.FirstName,
            LastName = practionerData.LastName,
            Email = practionerData.Email,
            StartDate = practionerData.StartDate,
            Earnings = practionerData.Earnings,
            Address = practionerData.Address,
            Latitude = practionerData.Latitude,
            Longitude = practionerData.Longitude,
        };

        return practionerDto;
    }
}