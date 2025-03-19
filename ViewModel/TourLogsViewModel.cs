using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using tour_planner.Commands;
using tour_planner.Model;
using tour_planner.View;

namespace tour_planner.ViewModel
{
    public class TourLogsViewModel : ViewModelBase
    {

        private TourModel _selectedTour;
        public TourModel SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;
              OnPropertyChanged(nameof(SelectedTour));

                // Update logs when the tour changes
                if (_selectedTour != null)
                {
                    TourLogs = _selectedTour.TourLogs;
                    //OnPropertyChanged(nameof(TourLogs));
                }
            }
        }
        public ICommand OpenEditPage { get; set; }
        public ICommand OpenNewPage { get; set; }
        public ICommand DeleteCommand { get; set; }
        private TourListViewModel TourListViewModel {get; set;}

        public TourLogsViewModel(TourListViewModel tourListViewModel)
        {
            TourLogs = new ObservableCollection<TourLogsModel>();
            this.TourListViewModel = tourListViewModel;
            tourListViewModel.OnTourSelected += HandleTourSelected;
            OpenEditPage = new RelayCommand(DoOpenEditPage, CanOpenEditPage);
            OpenNewPage = new RelayCommand(DoOpenNewPage, CanOpenNewPage);
            DeleteCommand = new RelayCommand(DoDelete, CanDelete);
        }
        private ObservableCollection<TourLogsModel> _tourLogs;

        public ObservableCollection<TourLogsModel> TourLogs
        {
            get => _tourLogs;
            set
            {
                _tourLogs = value;
                OnPropertyChanged(nameof(TourLogs));
            }
        }


        private void DoOpenNewPage(object obj)
        {
            var newLog = new TourLogsModel(DateTime.Now, 0, 0);
            var dialog = new AddEditTourLogsView
            {
                DataContext = new AddEditTourLogViewModel(newLog, true)
            };

            if (dialog.ShowDialog() == true)
            {
                TourLogs.Add(newLog);
            }
        }

        public bool CanOpenNewPage(object obj)
        {
            if (this.TourListViewModel.SelectedTour == null) return false;
            return true;
        }


        private void DoOpenEditPage(object obj)
        {
            var dialog = new AddEditTourLogsView
            {
                DataContext = new AddEditTourLogViewModel(SelectedLog, true)
            };

            if (dialog.ShowDialog() == true)
            {
                CollectionViewSource.GetDefaultView(TourLogs).Refresh(); //change force refresh
            }
        }

        public bool CanOpenEditPage(object obj)
        {
            if(SelectedLog == null) return false;
            return true;
        }

        private void DoDelete(object obj)
        {
            TourLogs.Remove(SelectedLog);
        }

        private bool CanDelete(object obj)
        {
            if (SelectedLog == null) return false;
            return true;
        }

        private void HandleTourSelected(object sender, TourModel selectedTour)
        {
            Debug.WriteLine($"Selected Tour: {selectedTour.Name}");
            SelectedTour = selectedTour;
        }




        private TourLogsModel _selectedLog;
        public TourLogsModel SelectedLog
        {
            get => _selectedLog;
            set
            {
                _selectedLog = value;
                OnPropertyChanged(nameof(SelectedLog));
            }
        }




   
    }
}
