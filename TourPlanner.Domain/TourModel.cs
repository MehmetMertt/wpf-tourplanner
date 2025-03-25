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

        public TourModel(string name = "New Tour", string date = "DD.MM.YYYY", string duration = "0", float distance = 0f)
        {
            Id = Guid.NewGuid();
            Name = name;
            Date = date;
            TotalDuration = duration;
            TotalDistance = distance;
            ImagePath = "";
            Description = "";
        }

    }
}
