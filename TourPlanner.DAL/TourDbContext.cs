using Microsoft.EntityFrameworkCore;
using TourPlanner.Domain;
using TourPlanner.DAL.Dto;
using System.Globalization;

namespace TourPlanner.DAL
{

    public class TourDbContext : DbContext
    {
        public TourDbContext(DbContextOptions options) : base(options)
        {
        }


        //lost the link to stackoverflow
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the value converter for TourLogsDto.Date to use DD.MM.YYYY in DB
            modelBuilder.Entity<TourLogsDto>()
                .Property(tl => tl.Date)
                .HasConversion(
                    // From C# DateTime to DB string (DD.MM.YYYY)
                    v => v.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture),
                    // From DB string (DD.MM.YYYY) to C# DateTime
                    v => DateTime.ParseExact(v, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None)
                );

            // Ensure Tour Logs have a foreign key to Tours
            modelBuilder.Entity<TourLogsDto>()
                .HasOne(tl => tl.Tour)
                .WithMany(t => t.TourLogs) 
                .HasForeignKey(tl => tl.TourId)
                .IsRequired();
        }

        public DbSet<TourLogsDto> TourLogs { get; set; }
        public DbSet<TourDto> Tours { get; set; }
    }
}
