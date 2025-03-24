using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tour_planner.Model
{
    public class TourLogsModel
    {

        public DateTime Date { get; set; }
        public float Duration { get; set; }
        public float Distance { get; set; }

        public TourLogsModel(DateTime date, float duration, float distance) {
            Date = date;
            Duration = duration;
            Distance = distance;
        }


    }
}
