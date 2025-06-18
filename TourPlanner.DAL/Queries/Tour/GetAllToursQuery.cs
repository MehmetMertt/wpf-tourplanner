using TourPlanner.Domain;
using TourPlanner.DAL.Dto;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace TourPlanner.DAL.Queries.Tour
{
    public class GetAllToursQuery
    {
        private readonly TourDbContextFactory _contextFactory;

        public GetAllToursQuery(TourDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<TourModel>> Execute()
        {
            using (TourDbContext context = _contextFactory.Create())
            {
                IEnumerable<TourDto> tourModelDtos = await context.Tours
                .Include(t => t.TourLogs)
                .ToListAsync();

                return tourModelDtos.Select(t => new TourModel(t.Id, t.Name, t.Date, t.TotalDuration, t.TotalDistance, t.ImagePath, t.Description,t.From,t.To,t.TransportType,
                    new ObservableCollection<TourLogsModel>(
                    t.TourLogs.Select(log => new TourLogsModel(
                        log.Id,
                        log.Date,
                        log.Duration,
                        log.Distance,
                        log.Comment,
                        log.Difficulty,
                        log.Rating,
                        log.TourId
                    ))
                )));

            }
        }
    }
}
