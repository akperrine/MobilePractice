using MobilePractice.Data;
using MobilePractice.Models;
using BCrypt.Net;
using MobilePractice.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace MobilePractice.Services;
public class PractitionerService {
    private readonly PracticeContext _context;

    public PractitionerService(PracticeContext context) {
        _context = context;
    }

    public async Task<List<Practitioner>> GetAll() => await _context.Practitioners.ToListAsync();

    public async Task<PractitionerDto?> GetPractitionerById(long id) {
        var practioner = await _context.Practitioners
                .FirstOrDefaultAsync(Practitioner => Practitioner.Id == id);

            if (practioner == null)
            {
                return null;
            }
            return mapPractitionerToDto(practioner);
    }

    public async Task<PractitionerDto?> RegisterPractitioner(Practitioner practitionerDto) {
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

        return mapPractitionerToDto(newPractitioner);
    }

    public async Task<PractitionerDto?> Login(LoginDto credentials) {
        var practioner = await _context.Practitioners.SingleAsync(prac => prac.Email == credentials.Email);
        if (practioner == null) {
            return null;
        };
        bool verifiedPassword = BCrypt.Net.BCrypt.Verify(credentials.Password, practioner.Password);
        if (!verifiedPassword) {
            return null;
        }
        return mapPractitionerToDto(practioner);
    }

    public async Task<PractitionerDto?> UpdatePractitioner(Practitioner practionerData) {
        _context.Practitioners.Update(practionerData);
        await _context.SaveChangesAsync();

        return mapPractitionerToDto(practionerData);
    }

    public async Task<PractitionerDto?> DeletePractitioner(long id) {
        var practioner = await _context.Practitioners.SingleAsync(prac => prac.Id == id);
        _context.Practitioners.Remove(practioner);
        await _context.SaveChangesAsync();

        return mapPractitionerToDto(practioner);
    }


    public PractitionerDto mapPractitionerToDto(Practitioner practitioner) {
        PractitionerDto practitionerDto = new PractitionerDto {
            Id = practitioner.Id,
            FirstName = practitioner.FirstName,
            LastName = practitioner.LastName,
            Email = practitioner.Email,
            StartDate = practitioner.StartDate,
            Earnings = practitioner.Earnings,
            Address = practitioner.Address,
            Latitude = practitioner.Latitude,
            Longitude = practitioner.Longitude,
        };
        return practitionerDto;
    }
}