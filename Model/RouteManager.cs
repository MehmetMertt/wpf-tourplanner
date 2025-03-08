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
            new Route()
            {
                Name = "Wienerwald"
            },
            new Route()
            {
                Name = "Dopplerhütte"
            }, 
            new Route()
            {
                Name = "Figlwarte"
            },
            new Route()
            {
                Name = "Dorfrunde"
            }
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
