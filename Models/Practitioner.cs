using Microsoft.AspNetCore.Identity;

namespace MobilePractice.Models;

public class Practitioner {
    public long Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set;}
    public string? Email { get; set;}
    public string? Password { get; set;}
    public DateTime StartDate { get; set;}
    public int Earnings { get; set;}
    public string? Address { get; set;}
    public float Latitude { get; set;}
    public float Longitude { get; set;}
}