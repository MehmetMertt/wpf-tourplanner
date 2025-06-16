using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain;
using TourPlanner.DAL.Dto;

namespace TourPlanner.DAL.Queries
{
    public class UpdateTourLogQuery
    {
        private readonly TourDbContextFactory _contextFactory;

        public UpdateTourLogQuery(TourDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(TourLogsModel tour)
        {
            using (TourDbContext context = _contextFactory.Create())
            {
                TourLogsDto tourDto = new TourLogsDto()
                {
                    Id = tour.Id,
                    Date = tour.Date,
                    Difficulty = tour.Difficulty,
                    Distance = tour.Distance,
                    Comment = tour.Comment,
                    Duration = tour.Duration,
                    Rating = tour.Rating,
                    TourId = tour.TourId
                  
                };

                context.TourLogs.Update(tourDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
