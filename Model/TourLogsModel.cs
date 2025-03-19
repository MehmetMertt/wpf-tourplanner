using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tour_planner.Model
{
    public class TourLogsModel
    {

        public string Date { get; set; }
        public string Duration { get; set; }
        public double Distance { get; set; }

        public TourLogsModel(string date, string duration, double distance) {
            Date = date;
            Duration = duration;
            Distance = distance;
        }


    }
}
