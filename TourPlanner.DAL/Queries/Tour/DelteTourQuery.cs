using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain;
using TourPlanner.DAL.Dto;

namespace TourPlanner.DAL.Queries.Tour
{
    public class DeleteTourQuery
    {
        private readonly TourDbContextFactory _contextFactory;

        public DeleteTourQuery(TourDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid id)
        {
            using (TourDbContext context = _contextFactory.Create())
            {
                TourDto tourDto = new TourDto()
                {
                    Id = id
                };

                context.Tours.Remove(tourDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
