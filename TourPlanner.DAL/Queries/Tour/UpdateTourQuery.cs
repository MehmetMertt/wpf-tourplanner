﻿using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain;
using TourPlanner.DAL.Dto;

namespace TourPlanner.DAL.Queries.Tour
{
    public class UpdateTourQuery
    {
        private readonly TourDbContextFactory _contextFactory;

        public UpdateTourQuery(TourDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(TourModel tour)
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
                };

                context.Tours.Update(tourDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
