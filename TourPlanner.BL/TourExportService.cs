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
        public Task ExportTourAsync(TourModel tour) //,TourLogsModel tourlogs
        {
            //     Debug.Write(tour.TourLogs[0].Id);
            Debug.Write($"TOURLOGS: {tour.TourLogs}, ENDE");
            Debug.Write($"TOURLOGS: {tour.TourLogs[0].Comment}, ENDE");
            string tourfileContent = JsonSerializer.Serialize(tour);
            // string tourlogfileContent = JsonSerializer.Serialize(tourlogs);
            //string wholeFileContent = tourfileContent + tourlogfileContent;
            string fileName = $"{tour.Name}_{tour.Date}.tourlog";
            string currentFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filePath = System.IO.Path.Combine(currentFolder, fileName);

            using (FileStream fs = File.Create(filePath, 1024))
            {
                byte[] tourinfo = new UTF8Encoding(true).GetBytes(tourfileContent);
                //byte[] tourinfo = new UTF8Encoding(true).GetBytes(wholeFileContent);
                fs.Write(tourinfo, 0, tourinfo.Length);
            }
            return Task.CompletedTask;
        }
    }
}
