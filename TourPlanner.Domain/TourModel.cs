using System.Collections.ObjectModel;

namespace TourPlanner.Domain
{
    public class TourModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string TotalDuration { get; set; }
        public float TotalDistance { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public ObservableCollection<TourLogsModel> TourLogs { get; set; } = new ObservableCollection<TourLogsModel>();


        public TourModel(Guid id, string name, string date, string totalDuration, float totalDistance, string imagePath, string description)
        {
            Id = id;
            Name = name;
            Date = date;
            TotalDuration = totalDuration;
            TotalDistance = totalDistance;
            ImagePath = imagePath;
            Description = description;
        }
    }
}
