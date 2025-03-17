using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using tour_planner.Commands;
using tour_planner.Model;
using tour_planner.View;

namespace tour_planner.ViewModel
{
    internal class TourListViewModel : ViewModelBase
    {
        public ObservableCollection<Tour> Tours { get; set; } = new ObservableCollection<Tour>(); // to store (reference) all the tours from TourManager

        public ICommand ShowWindowCommand { get; set; }



        public TourListViewModel()
        {


            DeleteTourCommand = new RelayCommand(DoDeleteTour, CanDeleteTour);
            OpenAddPage = new RelayCommand(OpenAddTour, CanOpenAddTour);
            OpenEditPage = new RelayCommand(OpenEditTour, CanOpenEditTour);

            LoadTours();
        }


        public ICommand AddTourCommand { get; set; } // command to link button to a function to add a new tour
        public ICommand DeleteTourCommand { get; set; } // command to link button to a function to delete an existing route 
        public ICommand OpenAddPage { get; set; }
        public ICommand OpenEditPage { get; set; }


        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;
               OnPropertyChanged(nameof(SelectedTour)); // to update entries in data grid
            }
        }


        private bool CanOpenEditTour(object obj)
        {
            if(_selectedTour is null)
            {
                return false;
            } else
            {
                return true;
            }
        }

        private void OpenEditTour(object obj)
        {
            var dialog = new AddEditTour
            {
                DataContext = new AddEditTourViewModel(_selectedTour)
            };

            dialog.ShowDialog();
        }

        private bool CanOpenAddTour(object obj)
        {
            return true;
        }

        private void OpenAddTour(object obj)
        {
            Tour newTour = new Tour();
        var dialog = new AddEditTour()
        {
            DataContext = new AddEditTourViewModel(newTour)
            };
            if(dialog.ShowDialog() == true)
            {
                Tours.Add(newTour);
            }
        }


      

        private void DoDeleteTour(object obj)
        {

            TourManager.DeleteTour(SelectedTour);
        }

        private bool CanDeleteTour(object obj)
        {
            if (SelectedTour != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        private void LoadTours()
        {
            Tours = TourManager.getTours();
        }
    }
}
