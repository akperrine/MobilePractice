
namespace MobilePractice.Models;

public class Treatment {
    public long Id {get; set;}
    public long PractitionerId {get; set;}
    public string? Name {get; set;}
    public int Duration {get; set;}
}