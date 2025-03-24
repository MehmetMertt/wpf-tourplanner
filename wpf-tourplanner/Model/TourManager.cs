using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace tour_planner.Model
{
    internal class TourManager
    {
        public static ObservableCollection<TourModel> _DatabaseRoute = new ObservableCollection<TourModel>()
        {
            new TourModel("Wienerwald", "01.01.2025", "0",123.2f),
            new TourModel("Dopplerhütte", "02.02.2025", "0",  223f), 
            new TourModel("Figlwarte", "03.03.2025"),

            new TourModel("Dorfrunde", "04.04.2025", "0", 1)
        };

        public TourManager()
        {

        }

        public static ObservableCollection<TourModel> getTours()
        {
            return _DatabaseRoute;
        }

        public static void AddTour(TourModel tour)
        {
            _DatabaseRoute.Add(tour);
        }

        public static void DeleteTour(TourModel tour)
        {
            _DatabaseRoute.Remove(tour);
        }
    }
}
