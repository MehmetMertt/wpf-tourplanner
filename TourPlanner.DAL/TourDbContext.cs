using Microsoft.EntityFrameworkCore;
using TourPlanner.Domain;
using TourPlanner.DAL.Dto;

namespace TourPlanner.DAL
{

    public class TourDbContext : DbContext
    {
        public TourDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<TourLogsDto> TourLogs { get; set; }
        public DbSet<TourDto> Tours { get; set; }
    }
}
