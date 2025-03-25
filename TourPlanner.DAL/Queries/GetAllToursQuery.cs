using TourPlanner.Domain;
using TourPlanner.DAL.Dto;
using Microsoft.EntityFrameworkCore;

namespace TourPlanner.DAL.Queries
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
            using(TourDbContext context =  _contextFactory.Create())
            {
                IEnumerable<TourDto> tourModelDtos = await context.Tours.ToListAsync();
                return tourModelDtos.Select(t => new TourModel(t.Id,t.Name, t.Date, t.TotalDuration, t.TotalDistance,t.ImagePath,t.Description));

            }
        }
    }
}
