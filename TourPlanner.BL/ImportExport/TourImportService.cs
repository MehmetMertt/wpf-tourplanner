using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TourPlanner.Domain;

namespace TourPlanner.BL.ImportExport
{
    public class TourImportService : ITourImportService
    {

        public Task<TourModel> ImportToursAsync(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);
            TourModel tour = JsonSerializer.Deserialize<TourModel>(jsonString);
            return Task.FromResult(tour);
        }
    }
}
