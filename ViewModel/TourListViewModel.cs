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
        public ObservableCollection<TourModel> Tours { get; set; } = new ObservableCollection<TourModel>(); // to store (reference) all the tours from TourManager

        public ICommand ShowWindowCommand { get; set; }



        public TourListViewModel()
        {

            DeleteTourCommand = new RelayCommand(DoDeleteTour, CanDeleteTour);
            OpenAddPage = new RelayCommand(OpenAddTour, CanOpenAddTour);
            OpenEditPage = new RelayCommand(OpenEditTour, CanOpenEditTour);
            OpenDetailsPage = new RelayCommand(OpenViewPage, CanOpenViewPage);
            LoadTours();
        }


        public ICommand AddTourCommand { get; set; } // command to link button to a function to add a new tour
        public ICommand DeleteTourCommand { get; set; } // command to link button to a function to delete an existing route 
        public ICommand OpenAddPage { get; set; }
        public ICommand OpenEditPage { get; set; }
        public ICommand OpenDetailsPage { get; set; }
        public event Action<object, TourModel> OnTourSelected;


        private TourModel _selectedTour;
        public TourModel SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour)); // to update entries in data grid
                if (_selectedTour != null)
                {                  
                    // This can be used to update other views when the tour is selected
                    OnTourSelected?.Invoke(this, _selectedTour);
                }
            }


        }



        private void OpenViewPage(object obj)
        {
            var dialog = new AddEditTour
            {
                DataContext = new AddEditTourViewModel(_selectedTour, false)
            };

            dialog.ShowDialog();
        }


        private bool CanOpenViewPage(object obj)
        {
            return true;
        }


        private bool CanOpenEditTour(object obj)
        {
            if (_selectedTour is null)
            {
                return false;
            }
            else
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
            TourModel newTour = new TourModel();
            var dialog = new AddEditTour()
            {
                DataContext = new AddEditTourViewModel(newTour)
            };
            if (dialog.ShowDialog() == true)
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

            if (Tours.Count > 0)
            {
                Tours[0].TourLogs.Add(new TourLogsModel("01.01.2025", "2h", 20.5));
                Tours[0].TourLogs.Add(new TourLogsModel("02.01.2025", "3h", 25.1));
            }

            if (Tours.Count > 1)
            {
                Tours[1].TourLogs.Add(new TourLogsModel("02.01.2025", "23h", 20.5));
                Tours[1].TourLogs.Add(new TourLogsModel("02.01.2025", "1h", 25.1));
            }
        }
    }
}
