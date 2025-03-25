
using System.Collections.ObjectModel;


namespace TourPlanner.DAL.Dto
{
    public class TourDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string TotalDuration { get; set; }
        public float TotalDistance { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public ObservableCollection<TourLogsDto> TourLogs { get; set; } = new ObservableCollection<TourLogsDto>();
    }
}
