using TourPlanner.Domain;

namespace TourPlanner.BL.ImportExport
{
    public interface ITourImportService
    {
        Task<TourModel> ImportToursAsync(string filePath);
    }

}
