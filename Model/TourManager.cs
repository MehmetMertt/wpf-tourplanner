using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace tour_planner.Model
{
    internal class TourManager
    {
        // attributes:
        public static ObservableCollection<Tour> _DatabaseRoute = new ObservableCollection<Tour>()
        {
            new Tour("Wienerwald", "01.01.2025", "0",123.123),
            new Tour("Dopplerhütte", "02.02.2025", "0",  222.22), 
            new Tour("Figlwarte", "03.03.2025"),
            new Tour("Dorfrunde", "04.04.2025", "0", 1)
        };

        public TourManager()
        {
            // constructor ...
        }

        // methods:
        public static ObservableCollection<Tour> getTours()
        {
            return _DatabaseRoute;
        }

        public static void AddTour(Tour tour)
        {
            _DatabaseRoute.Add(tour);
        }

        public static void DeleteTour(Tour tour)
        {
            _DatabaseRoute.Remove(tour);
        }
    }
}
