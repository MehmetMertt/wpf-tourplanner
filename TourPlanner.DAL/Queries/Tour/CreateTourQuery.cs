using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain;
using TourPlanner.DAL.Dto;
using log4net;
using System.Linq.Expressions;

namespace TourPlanner.DAL.Queries.Tour
{
    public class CreateTourQuery
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CreateTourQuery));

        private readonly TourDbContextFactory _contextFactory;

        public CreateTourQuery(TourDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(TourModel tour)
        {
            try
            {
                using (TourDbContext context = _contextFactory.Create())
                {
                    TourDto tourDto = new TourDto()
                    {
                        Id = tour.Id,
                        Name = tour.Name,
                        Date = tour.Date,
                        Description = tour.Description,
                        ImagePath = tour.ImagePath,
                        TotalDuration = tour.TotalDuration,
                        TotalDistance = tour.TotalDistance,
                        From = tour.From,
                        To = tour.To,
                        TransportType = tour.TransportType,
                        TourLogs = new System.Collections.ObjectModel.ObservableCollection<TourLogsDto>(
                         tour.TourLogs.Select(log => new TourLogsDto
                         {
                             Id = log.Id,
                             Date = log.Date,
                             Duration = log.Duration,
                             Distance = log.Distance,
                             Comment = log.Comment,
                             Difficulty = log.Difficulty,
                             Rating = log.Rating,
                             TourId = tour.Id
                         })
                     )
                    };

                    context.Tours.Add(tourDto);
                    await context.SaveChangesAsync();
                    log.Info($"Tour {tour.Name} successfully created");
                }
            }
            catch (Exception e)
            {
                log.Warn($"Creating Tour {tour.Name} failed: {e}");
            }
          
        }
    }
}
