using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Input;
using tour_planner.Commands;
using tour_planner.Model;
using tour_planner.View;
using TourPlanner.Domain;


namespace tour_planner.ViewModel
{
    public class TourListViewModel : ViewModelBase
    {
        public ObservableCollection<TourModel> Tours { get; set; } = new ObservableCollection<TourModel>();

        public ICommand ShowWindowCommand { get; set; }

        private TourManager _tourManager { get; set; }

        public TourListViewModel(TourManager tourManager)
        {
            _tourManager = tourManager;
            DeleteTourCommand = new RelayCommand(DoDeleteTour, CanDeleteTour);
            OpenAddPage = new RelayCommand(OpenAddTour, CanOpenAddTour);
            OpenEditPage = new RelayCommand(OpenEditTour, CanOpenEditTour);
            OpenDetailsPage = new RelayCommand(OpenViewPage, CanOpenViewPage);
            LoadTours();
            Debug.WriteLine(Tours.ToString());
            Debug.WriteLine(Tours.ToString());
            Debug.WriteLine(Tours.ToString());
            Debug.WriteLine(Tours.ToString());
            Debug.WriteLine(Tours.ToString());
        }


        public ICommand AddTourCommand { get; set; } // command to link button to a function to add a new tour
        public ICommand DeleteTourCommand { get; set; } // command to link button to a function to delete an existing route 
        public ICommand OpenAddPage { get; set; }
        public ICommand OpenEditPage { get; set; }
        public ICommand OpenDetailsPage { get; set; }

        public event EventHandler<TourModel> OnTourSelected;

        private TourModel _selectedTour;
        public virtual TourModel SelectedTour
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
            var dialog = new EditTourView
            {
                DataContext = new EditTourViewModel(_selectedTour, _tourManager, false)
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
            var dialog = new EditTourView
            {
                DataContext = new EditTourViewModel(_selectedTour, _tourManager)
            };

            if (dialog.ShowDialog() == true)
            {
                LoadTours();
            }
        }

        private bool CanOpenAddTour(object obj)
        {
            return true;
        }

        private void OpenAddTour(object obj)
        {
            TourModel newTour = new TourModel(Guid.NewGuid(), "Name", "DD.MM.YYYY", 0f, 0f, "", "", "", "", "");
            var dialog = new AddTourView()
            {
                DataContext = new AddTourViewModel(newTour, _tourManager)
            };
            if (dialog.ShowDialog() == true)
            {
                Tours.Add(newTour);
            }
        }




        private void DoDeleteTour(object obj)
        {

            _tourManager.DeleteTour(_selectedTour.Id);
            var tourToRemove = Tours.FirstOrDefault(y => y.Id == _selectedTour.Id);
            if (tourToRemove != null)
            {
                Tours.Remove(tourToRemove);
            }
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



        private async void LoadTours()
        {

            var tours = await _tourManager.getTours();
            Tours.Clear();
            foreach (var tour in tours)
            {
                Tours.Add(tour);
            }
        }
    }
}
