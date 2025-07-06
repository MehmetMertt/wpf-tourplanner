
namespace TourPlanner.Domain
{
    public interface ITourLogsModel
    {
        string Comment { get; set; }
        DateTime Date { get; }
        string DateString { get; set; }
        string Difficulty { get; set; }
        float Distance { get; }
        string DistanceString { get; set; }
        float Duration { get; }
        string DurationString { get; set; }
        Guid Id { get; set; }
        int Rating { get; set; }
        Guid TourId { get; set; }

        TourLogsModel WithComment(string comment);
        TourLogsModel WithDate(DateTime date);
        TourLogsModel WithDifficulty(string difficulty);
        TourLogsModel WithDistance(float distance);
        TourLogsModel WithDuration(float duration);
        TourLogsModel WithId(Guid id);
        TourLogsModel WithRating(int rating);
        TourLogsModel WithTourId(Guid tourId);
    }
}