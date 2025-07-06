using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain;
using TourPlanner.DAL.Dto;
using log4net;

namespace TourPlanner.DAL.Queries
{
    public class UpdateTourLogQuery
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CreateTourLogQuery));

        private readonly TourDbContextFactory _contextFactory;

        public UpdateTourLogQuery(TourDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(TourLogsModel tour)
        {
            try
            {
                using (TourDbContext context = _contextFactory.Create())
                {
                    TourLogsDto tourDto = new TourLogsDto()
                    {
                        Id = tour.Id,
                        Date = tour.Date.ToUniversalTime(),
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
                log.Info($"Tour log with the id {tour.Id} updated successfully in the Database");

            }
            catch (Exception e)
            {
                log.Warn($"Tour log with the id {tour.Id} update failed in the Database: {e.ToString} ");

                throw e;
            }
        }
    }
}
