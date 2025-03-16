using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace tour_planner.Model
{
    internal class RouteManager
    {
        public static ObservableCollection<Route> _DatabaseRoute = new ObservableCollection<Route>()
        {
            new Route("Wienerwald"),
            new Route("Dopplerhütte"),
            new Route("Figlwarte"),
            new Route("Dorfrunde")
        };

        public static ObservableCollection<Route> getRoutes()
        {
            return _DatabaseRoute;
        }

        public static void AddRoutes(Route route)
        {
            _DatabaseRoute.Add(route);
        }
    }
}
