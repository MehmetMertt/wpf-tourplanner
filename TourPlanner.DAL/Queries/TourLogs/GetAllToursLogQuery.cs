using TourPlanner.Domain;
using TourPlanner.DAL.Dto;
using Microsoft.EntityFrameworkCore;

namespace TourPlanner.DAL.Queries
{
    public class GetAllToursLogQuery
    {
        private readonly TourDbContextFactory _contextFactory;

        public GetAllToursLogQuery(TourDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<TourLogsModel>> Execute()
        {
            using(TourDbContext context =  _contextFactory.Create())
            {
                IEnumerable<TourLogsDto> tourLogsModelDtos = await context.TourLogs.ToListAsync();
                return tourLogsModelDtos.Select(t => new TourLogsModel(t.Id,t.Date,t.Duration,t.Distance,t.Comment,t.Difficulty,t.Rating,t.TourId));

            }
        }
    }
}
