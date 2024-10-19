using MobilePractice.Data;
using MobilePractice.Models;
using Microsoft.EntityFrameworkCore;

namespace MobilePractice.Services;
public class AppointmentService {
    private readonly PracticeContext _context;

    public AppointmentService(PracticeContext context) {
        _context = context;
    }

    public async Task<List<Appointment>> GetAll() => await _context.Appointments.ToListAsync();

    public async Task<Appointment?> GetAppointmentById(long id) {
        var appointment = await _context.Appointments
                .FirstOrDefaultAsync(t => t.Id == id);

            if (appointment == null)
            {
                return null;
            }
            return appointment;
    }

    public async Task<Appointment?> CreateAppointment(Appointment appointment) {
        var existingAppointment = await _context.Appointments.Where(a => a.Id == appointment.Id).FirstOrDefaultAsync();

        if (existingAppointment != null) {
            return null;
        };
        Appointment newAppointment = appointment;

        await _context.AddAsync(newAppointment);
        await _context.SaveChangesAsync();

        return newAppointment;
    }

    public async Task<Appointment?> UpdateAppointment(Appointment appointment) {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();

        return appointment; 
    }

    public async Task<Appointment?> DeleteAppointment(long id) {
        var appointment = await _context.Appointments.SingleAsync(t => t.Id == id);
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();

        return appointment;
    }
}