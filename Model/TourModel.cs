using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace tour_planner.Model
{
    public class TourModel
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string TotalDuration { get; set; }
        public float TotalDistance { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public ObservableCollection<TourLogsModel> TourLogs { get; set; } = new ObservableCollection<TourLogsModel>();

        public TourModel(string name = "New Tour", string date = "DD.MM.YYYY", string duration = "0", float distance = 0f)
        {
            Name = name;
            Date = date;
            TotalDuration = duration;
            TotalDistance = distance;
            ImagePath = "";
            Description = "";
        }

    }
}
