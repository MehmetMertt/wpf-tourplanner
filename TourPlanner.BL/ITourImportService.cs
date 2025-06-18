using TourPlanner.Domain;

namespace TourPlanner.BL
{
    public interface ITourImportService
    {
        Task<TourModel> ImportToursAsync(string filePath);
    }

}
