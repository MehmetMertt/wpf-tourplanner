using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace tour_planner.Model
{
    public class Tour
    {
        // attributes:
        public string Name { get; set; }
        public string Date { get; set; }
        public string TotalDuration { get; set; } // total duration of all routes
        public double TotalDistance { get; set; } // total distance of all routes
        // public string Description { get; set; }
        public List<Route> Routes { get; set; } = new List<Route>(); // a tour can have multiple routes

        public Tour(string name = "New Tour", string date = "DD.MM.YYYY", string duration = "0", float distance = 0f)
        {
            // constructor:
            Name = name;
            Date = date;
            TotalDuration = duration;
            TotalDistance = distance;
        }

        // methods:
    }
}
