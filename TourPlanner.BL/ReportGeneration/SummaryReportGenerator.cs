using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Domain;
using static System.Net.Mime.MediaTypeNames;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace TourPlanner.BL.ReportGeneration
{
    public class SummaryReportGenerator
    {
        public static void GenerateSummaryReport(List<TourModel> tours, string filePath)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Size(PageSizes.A4);
                    page.Header().Text("Tour Summary Report").FontSize(20).Bold();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();

                            //columns.ConstantColumn(80);
                            //columns.ConstantColumn(80);
                            //columns.ConstantColumn(80);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Tour Name").Bold();
                            
                            header.Cell().Text("Min Time").Bold();
                            header.Cell().Text("Avg Time").Bold();
                            header.Cell().Text("Max Time").Bold();

                            header.Cell().Text("Min Dist").Bold();
                            header.Cell().Text("Avg Dist").Bold();
                            header.Cell().Text("Max Dist").Bold();
                            
                            header.Cell().Text("Avg Rating").Bold();
                        });


                        foreach (var tour in tours)
                        {
                            var logs = tour.TourLogs;
                            if (logs == null || logs.Count == 0) continue;

                            var minDuration = logs.Min(l => l.Duration);
                            var avgDuration = logs.Average(l => l.Duration);
                            var maxDuration = logs.Max(l => l.Duration);

                            var minDistance = logs.Min(l => l.Distance);
                            var avgDistance = logs.Average(l => l.Distance);
                            var maxDistance = logs.Max(l => l.Distance);

                            double avgRating = logs.Average(l => l.Rating);

                            table.Cell().Text(tour.Name);
                            table.Cell().Text($"{minDuration:F1} h");
                            table.Cell().Text($"{avgDuration:F1} h");
                            table.Cell().Text($"{maxDuration:F1} h");

                            table.Cell().Text($"{minDistance:F1} km");
                            table.Cell().Text($"{avgDistance:F1} km");
                            table.Cell().Text($"{maxDistance:F1} km");

                            table.Cell().Text($"{avgRating:F1}/5");
                        }
                    });
                });
            }).GeneratePdf(filePath);
        }
    }
}
