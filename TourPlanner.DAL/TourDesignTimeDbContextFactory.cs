using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DAL
{
    public class TourDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TourDbContext>
    {





        public TourDbContext CreateDbContext(string[] args = null) {
            string connectionString = "Host=localhost;Username=user;Password=password;Database=tour_db";
            return new TourDbContext(new DbContextOptionsBuilder().UseNpgsql(connectionString).Options);
        }
    }
}
