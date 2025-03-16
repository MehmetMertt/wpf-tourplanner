using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using tour_planner.Commands;
using tour_planner.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace tour_planner.ViewModel
{
    internal class RouteViewModel : ViewModelBase
    {
        // attributes:
        public ObservableCollection<Tour> Tours { get; set; } = new ObservableCollection<Tour>(); // to store (reference) all the tours from TourManager

        public ICommand ShowWindowCommand { get; set; }
        private Tour _selectedTour;
        public Tour SelectedTour // getter & setter
        {
            get => _selectedTour;
            set 
            { 
                _selectedTour = value; 
                OnPropertyChanged(); 
                OnPropertyChanged(nameof(SelectedTour)); // to update entries in data grid
            }
        }

        public ICommand AddTourCommand { get; } // command to link button to a function to add a new tour
        public ICommand DeleteTourCommand { get; } // command to link button to a function to delete an existing route 

        public RouteViewModel()
        {
            // constructor:
            LoadTours();
            AddTourCommand = new RelayCommand(AddTour);
            DeleteTourCommand = new RelayCommand(DeleteTour, () => SelectedTour != null);

            // ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);
        }

        // methods:

        private void LoadTours()
        {
            Tours = TourManager.getTours();
        }
        private void AddTour()
        {
            Tour newTour = new Tour();
            TourManager.AddTour(newTour);
        }

        private void DeleteTour()
        {
            if (SelectedTour != null)
            {
                TourManager.DeleteTour(SelectedTour);
                Tours.Remove(SelectedTour);
            }
        }

        private bool CanShowWindow(object obj)
        {
           return true;
        }

        private void ShowWindow(object obj)
        {
            // show window
        }
    }
}
