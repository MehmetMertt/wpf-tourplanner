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

                return tourModelDtos.Select(t =>
     new TourModel()
         .WithId(t.Id)
         .WithName(t.Name)
         .WithDate(t.Date)
         .WithDuration(t.TotalDuration)
         .WithDistance(t.TotalDistance)
         .WithImagePath(t.ImagePath)
         .WithDescription(t.Description)
         .WithFrom(t.From)
         .WithTo(t.To)
         .WithTransportType(t.TransportType)
         .WithTourLogs(new ObservableCollection<TourLogsModel>(
             t.TourLogs.Select(log => new TourLogsModel()
                 .WithId(log.Id)
                 .WithDate(log.Date)
                 .WithDuration(log.Duration)
                 .WithDistance(log.Distance)
                 .WithComment(log.Comment)
                 .WithDifficulty(log.Difficulty)
                 .WithRating(log.Rating)
                 .WithTourId(log.TourId)
             )
         ))
 );


            }
        }
    }
}
