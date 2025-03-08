using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tour_planner.Commands;
using tour_planner.Model;

namespace tour_planner.ViewModel
{
    internal class RouteViewModel
    {
        public ObservableCollection<Route> Routes { get; set; }

        public ICommand ShowWindowCommand { get; set; }

        public RouteViewModel()
        {
            Routes = RouteManager.getRoutes();

          //  ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);
        }

        private bool CanShowWindow(object obj)
        {
           return true;
        }

        private void ShowWindow(object obj)
        {
    
        }
    }
}
