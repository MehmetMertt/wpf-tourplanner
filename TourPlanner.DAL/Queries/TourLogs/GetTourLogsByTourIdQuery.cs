using Microsoft.EntityFrameworkCore;
using System.Linq;
using TourPlanner.DAL;
using TourPlanner.DAL.Dto;
using TourPlanner.Domain;

public class GetTourLogsByTourIdQuery
{
    private readonly TourDbContextFactory _contextFactory;

    public GetTourLogsByTourIdQuery(TourDbContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<TourLogsModel>> Execute(Guid tourId)
    {
        using var context = _contextFactory.Create();

  

        IEnumerable<TourLogsDto> tourLogsModelDtos = await context.TourLogs.Where(log => log.TourId == tourId).ToListAsync();


        return tourLogsModelDtos.Select(log =>
      new TourLogsModel(
          log.Id,
          log.Date,
          log.Duration,
          log.Distance,
          log.Comment,
          log.Difficulty,
          log.Rating,
          log.TourId
      )
  );
    }
}
