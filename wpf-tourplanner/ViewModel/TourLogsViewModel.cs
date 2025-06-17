using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using tour_planner.Commands;
using tour_planner.Model;
using tour_planner.View;
using tour_planner.ViewModel;
using TourPlanner.Domain;
using TourPlanner.Model;

namespace tour_planner.ViewModel
{
    public class TourLogsViewModel : ViewModelBase
    {
        private TourModel _selectedTour;
        private readonly TourLogsManager _tourLogsManager;
        private ObservableCollection<TourLogsModel> _tourLogs;
        private TourLogsModel _selectedLog;

        private TourListViewModel TourListViewModel { get; }

        public ObservableCollection<TourLogsModel> TourLogs
        {
            get => _tourLogs;
            set
            {
                _tourLogs = value;
                OnPropertyChanged(nameof(TourLogs));
            }
        }

        public TourLogsModel SelectedLog
        {
            get => _selectedLog;
            set
            {
                _selectedLog = value;
                OnPropertyChanged(nameof(SelectedLog));
            }
        }

        public TourModel SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));

                // Load logs from DB
                if (_selectedTour != null)
                {
                    _ = LoadTourLogs(_selectedTour.Id);
                }
            }
        }

        public ICommand OpenEditPage { get; set; }
        public ICommand OpenNewPage { get; set; }
        public ICommand DeleteCommand { get; set; }

        public TourLogsViewModel(TourListViewModel tourListViewModel, TourLogsManager tourLogsManager)
        {
            _tourLogsManager = tourLogsManager;
            TourLogs = new ObservableCollection<TourLogsModel>();
            TourListViewModel = tourListViewModel;

            OpenEditPage = new RelayCommand(DoOpenEditPage, CanOpenEditPage);
            OpenNewPage = new RelayCommand(DoOpenNewPage, CanOpenNewPage);
            DeleteCommand = new RelayCommand(DoDelete, CanDelete);

            tourListViewModel.OnTourSelected += HandleTourSelected;

            if (tourListViewModel.SelectedTour != null)
            {
                HandleTourSelected(this, tourListViewModel.SelectedTour);
            }
        }

        private async Task LoadTourLogs(Guid tourId)
        {
            var logs = await _tourLogsManager.GetLogsForTour(tourId);
            TourLogs.Clear();

            foreach (var log in logs)
            {
                Debug.WriteLine($"{log} added to {tourId}");
                TourLogs.Add(log);
            }
        }

        private void HandleTourSelected(object sender, TourModel selectedTour)
        {
            Debug.WriteLine($"Selected Tour: {selectedTour.Name}");
            SelectedTour = selectedTour;
        }

        private void DoOpenNewPage(object obj)
        {
            var newLog = new TourLogsModel(Guid.NewGuid(), DateTime.UtcNow, 0, 0, "", "", 0, SelectedTour.Id);
            var dialog = new AddTourLogsView
            {
                DataContext = new AddTourLogViewModel(newLog, _tourLogsManager)
            };

            if (dialog.ShowDialog() == true)
            {
                TourLogs.Add(newLog);
            }
        }

        private bool CanOpenNewPage(object obj) => TourListViewModel.SelectedTour != null;

        private void DoOpenEditPage(object obj)
        {
            var dialog = new EditTourLogsView
            {
                DataContext = new EditTourLogViewModel(SelectedLog, _tourLogsManager)
            };

            if (dialog.ShowDialog() == true)
            {
                CollectionViewSource.GetDefaultView(TourLogs).Refresh();
            }
        }

        private bool CanOpenEditPage(object obj) => SelectedLog != null;

        private void DoDelete(object obj)
        {
            if (SelectedLog != null)
            {
                var selectedLogId = SelectedLog.Id;
                TourLogs.Remove(SelectedLog);
                _tourLogsManager.DeleteLog(selectedLogId); 
            }
        }

        private bool CanDelete(object obj) => SelectedLog != null;
    }
}
