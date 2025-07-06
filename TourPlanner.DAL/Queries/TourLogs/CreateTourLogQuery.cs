using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain;
using TourPlanner.DAL.Dto;
using log4net;
using System.Text.Json;

namespace TourPlanner.DAL.Queries
{
    public class CreateTourLogQuery
    {
        private readonly TourDbContextFactory _contextFactory;
        private static readonly ILog log = LogManager.GetLogger(typeof(CreateTourLogQuery));

        public CreateTourLogQuery(TourDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(TourLogsModel tourLog,Guid tourId)
        {
            try
            {
                using (TourDbContext context = _contextFactory.Create())
                {
                    TourLogsDto tourDto = new TourLogsDto()
                    {
                        Comment = tourLog.Comment,
                        Date = tourLog.Date.ToUniversalTime(),
                        Difficulty = tourLog.Difficulty,
                        Distance = tourLog.Distance,
                        Duration = tourLog.Duration,
                        Rating = tourLog.Rating,
                        Tour = null,
                        Id = tourLog.Id,
                        TourId = tourId
                    };

                    context.TourLogs.Add(tourDto);
                    await context.SaveChangesAsync();
                    string serializedTourLog = JsonSerializer.Serialize(tourDto, new JsonSerializerOptions { WriteIndented = true });
                    log.Info("Created tour log successfully: " + serializedTourLog);

                }
            }
            catch (Exception e)
            {
                log.Error("Error while creating tour log: " + e.ToString());
                throw e;
            }
        }
    }
}
