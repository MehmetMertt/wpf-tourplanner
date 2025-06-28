using log4net;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Win32;
using tour_planner.Commands;
using tour_planner.Model;
using tour_planner.View;
using tour_planner.ViewModel;
using TourPlanner.DAL.Queries;
using TourPlanner.Domain;
using TourPlanner.Model;
using TourPlanner.BL.ReportGeneration;

namespace tour_planner.ViewModel
{

    public class TourLogsViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CreateTourLogQuery));
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
        public ICommand SaveReport { get; set; }

        public TourLogsViewModel(TourListViewModel tourListViewModel, TourLogsManager tourLogsManager)
        {
            _tourLogsManager = tourLogsManager;
            TourLogs = new ObservableCollection<TourLogsModel>();
            TourListViewModel = tourListViewModel;

            OpenEditPage = new RelayCommand(DoOpenEditPage, CanOpenEditPage);
            OpenNewPage = new RelayCommand(DoOpenNewPage, CanOpenNewPage);
            DeleteCommand = new RelayCommand(DoDelete, CanDelete);
            SaveReport = new RelayCommand(DoGenerateReport, CanGenerateReport);

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
            log.Info($"Loaded {logs.Count} Tour Logs for Tour with id: {tourId}");

            foreach (var log in logs)
            {
                Debug.WriteLine($"{log} added to {tourId}");
                TourLogs.Add(log);
            }
        }

        private void HandleTourSelected(object sender, TourModel selectedTour)
        {
            log.Info($"User selected new Tour: {selectedTour.Name}");
            SelectedTour = selectedTour;
        }

        private void DoOpenNewPage(object obj)
        {
            var newLog = new TourLogsModel(Guid.NewGuid(), DateTime.UtcNow, 0, 0, "", "", 0, SelectedTour.Id);
            var dialog = new AddTourLogsView
            {
                DataContext = new AddTourLogViewModel(newLog, _tourLogsManager)
            };

            log.Info("User opened New Tour Log page");
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
            log.Info("User opened Edit Tour Log Page");

            if (dialog.ShowDialog() == true)
            {
                CollectionViewSource.GetDefaultView(TourLogs).Refresh();
            }
        }

        private bool CanOpenEditPage(object obj) => SelectedLog != null;

        private void DoDelete(object obj)
        {
            try
            {
                if (SelectedLog != null)
                {
                    var selectedLogId = SelectedLog.Id;
                    TourLogs.Remove(SelectedLog);
               
                    _tourLogsManager.DeleteLog(selectedLogId);

                    log.Info($"Tour Log with the id: {selectedLogId} deleted successfully from the database");
                } else
                {
                    log.Warn("Tried to delete Tour Log without selecting log");
                }


            }
            catch (Exception e)
            {
                log.Fatal("Unexpected behaviour while deleting log level: " + e);

            }
        }

        private bool CanDelete(object obj) => SelectedLog != null;

        private void DoGenerateReport(object obj)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = SelectedTour != null
                    ? $"{SelectedTour.Name.Replace(" ", "_")}_report{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                    : $"Summary_Report_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    if (SelectedTour != null) // if a tour is selected make normal report for that tour
                    {
                        TourReportGenerator.GenerateTourReport(SelectedTour, dialog.FileName);
                    }
                    else // if no tour is selected make summary report for all tours
                    {
                        var allTours = TourListViewModel.Tours.ToList();

                        SummaryReportGenerator.GenerateSummaryReport(allTours, dialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(
                        $"Error with report generation:\n{ex.Message}",
                        "Erro",
                        System.Windows.MessageBoxButton.OK,
                        System.Windows.MessageBoxImage.Error
                    );
                }
            }
        }

        private bool CanGenerateReport(object obj)
        {
            // generate report if a tour is selected or if there are any tours in the list 
            return SelectedTour != null || TourListViewModel.Tours.Any();
        }
    }
}
