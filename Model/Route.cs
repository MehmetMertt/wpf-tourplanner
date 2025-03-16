// put class TourData into separate file so it is not in "MainWindow.xaml.cs" but it is pfusched (see next comment)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tour_planner // changed namespace from "tour_planner.Model" to "tour_planner" so it works in "MainWindow.xaml.cs" (shit solution)
{
    public class Route
    {
        // attributes:
        public string Name { get; set; }
        public string Duration { get; set; } // time to complete the route or smth 
        public string StartLocation { get; set; } // the name of the location
        public string EndLocation { get; set; } // the name of the location
        public string Distance { get; set; } // the distance between starting location and end location
        public string ImagePath { get; set; }

        public Route()
        {
            // constructor ...
        }

        // methods:

    }
}