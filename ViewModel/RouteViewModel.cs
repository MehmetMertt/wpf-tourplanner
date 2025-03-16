using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using tour_planner.Commands;
using tour_planner.Model;
using tour_planner.View;

namespace tour_planner.ViewModel
{
    internal class RouteViewModel
    {
        public ObservableCollection<Route> Routes { get; set; }


        public ICommand AddNewRoute { get; set; }

        public ICommand EditRoute { get; set; }

        ///////////////////////////////////////////////////////////////

        public Route SelectedRoute { get; set; }

        ///////////////////////////////////////////////////////////////

        public RouteViewModel()
        {
            Routes = RouteManager.getRoutes();

            AddNewRoute = new RelayCommand(DoAddRoute, CanAddRoute);

            EditRoute = new RelayCommand(DoEditRoute, CanEditRoute);

        }


        private bool CanEditRoute(object obj)
        {
            return true; //derzeit noch keinen fall, wann man keine route hinzufügen könnte
        }

        private void DoEditRoute(object obj)
        {
            if (obj is not Route SelectedRoute) { // warum geht hier is not aber != nicht?!
                return;
            }


            var dialog = new AddRoute
            {
                ResponseText = SelectedRoute.Name,
            };


            if (dialog.ShowDialog() == true)
            {
                string newRoutenName = dialog.ResponseText;
                if (newRoutenName.Length <= 3)
                {
                    return;
                }
                // Routes.F
                SelectedRoute.Name = newRoutenName;
              //  Routes.Sele
            }
        }


        private bool CanAddRoute(object obj)
        {
            return true; //derzeit noch keinen fall, wann man keine route hinzufügen könnte
        }

        private void DoAddRoute(object obj)
        {
            var dialog = new AddRoute();
            if (dialog.ShowDialog() == true)
            {
                string RoutenName = dialog.ResponseText;
                if(RoutenName.Length <= 3)
                {
                    return;
                }

                var RouteItem = new Route(RoutenName);
                Routes.Add(RouteItem);
            }
        }

    }
}
