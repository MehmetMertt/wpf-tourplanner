using TourPlanner.Domain;

namespace TourPlanner.BL.OpenRouteServiceAPI
{
    public interface IOpenRouteServiceClient
    {
        Task<(float Distance, float DurationHours)> GetTimeDistance(VehicleProfile profile, TourModel tour);
    }
}