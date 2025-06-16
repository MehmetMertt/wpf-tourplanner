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
    public class DeleteTourLogQuery
    {
        private readonly TourDbContextFactory _contextFactory;

        public DeleteTourLogQuery(TourDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid id)
        {
            using (TourDbContext context = _contextFactory.Create())
            {
                TourLogsDto tourDto = new TourLogsDto()
                {
                    Id = id
                };

                context.TourLogs.Remove(tourDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
