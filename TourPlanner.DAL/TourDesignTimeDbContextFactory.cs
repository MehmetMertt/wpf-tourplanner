using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DAL
{
    public class TourDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TourDbContext>
    {


        public TourDbContext CreateDbContext(string[] args = null) {

            string host = ConfigurationManager.AppSettings["host"];
            string username = ConfigurationManager.AppSettings["username"];
            string password = ConfigurationManager.AppSettings["password"];
            string database = ConfigurationManager.AppSettings["database"];

            if(String.IsNullOrEmpty(host) || String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(database))
            {
                throw new Exception("Please fill in the configuration file");
            } 

            string connectionString = $"Host={host};Username={username};Password={password};Database={database}";
            return new TourDbContext(new DbContextOptionsBuilder().UseNpgsql(connectionString).Options);
        }
    }
}
