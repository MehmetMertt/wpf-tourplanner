using log4net;
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
        private static readonly ILog log = LogManager.GetLogger(typeof(TourImportService));


        public Task<TourModel> ImportToursAsync(string filePath)
        {
            try
            {
                var jsonString = File.ReadAllText(filePath);
                TourModel tour = JsonSerializer.Deserialize<TourModel>(jsonString);
                return Task.FromResult(tour);
            } catch(Exception e)
            {
                log.Warn($"Error while trying to import {filePath}: {e.ToString()}");
                throw e;
            }

        }
    }
}
