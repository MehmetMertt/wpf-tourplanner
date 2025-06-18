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

namespace TourPlanner.BL
{
    public class TourExportService : ITourExportService
    {
        public Task ExportTourAsync(TourModel tour)
        {
       //     Debug.Write(tour.TourLogs[0].Id);
            string fileContent = JsonSerializer.Serialize(tour);
            string fileName = $"{tour.Name}_{tour.Date}.tourlog";
            string currentFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filePath = System.IO.Path.Combine(currentFolder, fileName);

            using (FileStream fs = File.Create(filePath, 1024))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(fileContent);
                fs.Write(info, 0, info.Length);
            }
            return Task.CompletedTask;
        }
    }
}
