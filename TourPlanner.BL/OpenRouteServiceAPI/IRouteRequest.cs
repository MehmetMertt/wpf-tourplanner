
namespace TourPlanner.BL.OpenRouteServiceAPI
{
    public interface IRouteRequest
    {
        List<List<double>> Coordinates { get; set; }
        bool Instructions { get; set; }
        string Language { get; set; }
        string Units { get; set; }
    }
}