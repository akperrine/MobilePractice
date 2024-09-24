
using Microsoft.EntityFrameworkCore;
using MobilePractice.Models;


namespace MobilePractice.Data;

public class PracticeContext : DbContext {

    public PracticeContext(DbContextOptions<PracticeContext> options) : base(options) { }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         => optionsBuilder.UseNpgsql("Host=localhost;Database=practice-db;Username=postgres;Password=postgres>");
      
      public DbSet<Practitioner> Practitioners {get; set;}
}

