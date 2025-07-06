using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DAL
{
    public class TourDbContextFactory
    {

        private readonly DbContextOptions _options;


        public TourDbContextFactory(DbContextOptions options )
        {
            _options = options;
        }


        public TourDbContext Create() {

            return new TourDbContext(_options);
        }
    }
}
