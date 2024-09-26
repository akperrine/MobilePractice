
namespace MobilePractice.Models;

public class Appointment {
    public long Id {get; set;}
    public long PractitionerId {get; set;}
    public long ClientId {get; set;}
    public long ServiceId {get; set;}
    public DateTime startTime {get; set;}

}