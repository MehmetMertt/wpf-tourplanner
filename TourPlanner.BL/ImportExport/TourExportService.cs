using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TourPlanner.Domain;
using static System.Net.Mime.MediaTypeNames;

namespace TourPlanner.BL.ImportExport
{
    public class TourExportService : ITourExportService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TourExportService));

        public Task ExportTourAsync(TourModel tour)
        {
            try
            {
                string fileContent = JsonSerializer.Serialize(tour);
                string fileName = $"{tour.Name}_{tour.Date}.tourlog";
                string currentFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string filePath = Path.Combine(currentFolder, fileName);

                using (FileStream fs = File.Create(filePath, 1024))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(fileContent);
                    fs.Write(info, 0, info.Length);
                }
                log.Info($"{tour.Name} exported successfully to {filePath}");
                return Task.CompletedTask;
            } catch(Exception e)
            {
                log.Warn($"Exporting the Tour {tour.Name} failed: {e.ToString}");
                return Task.FromException(e);
            }

        }
    }
}
