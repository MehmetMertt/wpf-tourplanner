using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Domain
{
    public class TourLogsModel
    {

        Guid Id { get; set; }
        public DateTime Date { get; set; }
        public float Duration { get; set; }
        public float Distance { get; set; }

        public TourLogsModel(DateTime date, float duration, float distance) {
            Id = Guid.NewGuid();
            Date = date;
            Duration = duration;
            Distance = distance;
        }


    }
}
