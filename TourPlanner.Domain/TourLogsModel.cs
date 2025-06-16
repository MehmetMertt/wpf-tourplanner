using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Domain
{
    public class TourLogsModel
    {

        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public float Duration { get; set; }
        public float Distance { get; set; }
        public string Comment { get; set; }
        public string Difficulty { get; set; }
        public int Rating { get; set; }
        public Guid TourId { get; set; }


        public TourLogsModel(Guid id,DateTime date, float duration, float distance, string comment, string difficulty, int rating, Guid tourId)
        {
            Id = id;
            Date = date;
            Duration = duration;
            Distance = distance;
            Comment = comment;
            Difficulty = difficulty;
            Rating = rating;
            TourId = tourId;
        }


    }
}
