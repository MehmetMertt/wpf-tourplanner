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

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace TourPlanner.BL.ReportGeneration
{
    public class TourReportGenerator
    {
        public static void GenerateTourReport(TourModel tour, string filePath)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Size(PageSizes.A4);
                    page.Header()
                        .Text($"Tour Report: {tour.Name}")
                        .FontSize(20)
                        .Bold();

                    page.Content().Column(col =>
                    {
                        col.Spacing(10);

                        col.Item().Text($"From: {tour.From}");
                        col.Item().Text($"To: {tour.To}");
                        col.Item().Text($"Date: {tour.Date:d}");
                        col.Item().Text($"Distance: {tour.TotalDistance} km");
                        col.Item().Text($"Duration: {tour.TotalDuration} h");
                        col.Item().Text($"Transport: {tour.TransportType}");
                        col.Item().Text($"Description: {tour.Description}");

                        if (!string.IsNullOrEmpty(tour.ImagePath) && File.Exists(tour.ImagePath))
                        {
                            col.Item().Image(tour.ImagePath, ImageScaling.FitWidth);
                        }

                        col.Item().Text("Tour Logs:").Bold().FontSize(14);

                        foreach (var log in tour.TourLogs)
                        {
                            Debug.Write($"\n\n\n {log} \n\n\n");
                            col.Item().BorderBottom(1).PaddingBottom(5).PaddingTop(5).Column(logCol =>
                            {
                                logCol.Item().Text($"Date: {log.Date:d}");
                                logCol.Item().Text($"Comment: {log.Comment}");
                                logCol.Item().Text($"Difficulty: {log.Difficulty}");
                                logCol.Item().Text($"Rating: {log.Rating}");
                                //logCol.Item().Text($"Time: {log.TotalTime} h");
                            });
                        }
                    });
                });
            }).GeneratePdf(filePath);
        }
    }
}
