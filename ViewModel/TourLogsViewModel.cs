using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tour_planner.Model;

namespace tour_planner.ViewModel
{
    internal class TourLogsViewModel : ViewModelBase
    {
        public TourLogsViewModel(TourListViewModel tourListViewModel)
        {
            TourLogs = new ObservableCollection<TourLogsModel>();
            tourListViewModel.OnTourSelected += HandleTourSelected;
        }

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
                    OnPropertyChanged(nameof(TourLogs));
                }
            }
        }

        private void HandleTourSelected(object sender, TourModel selectedTour)
        {
            // Update properties or handle the tour selection here
            Debug.WriteLine($"Selected Tour: {selectedTour.Name}");
            SelectedTour = selectedTour;
            // You can update your TourLogsViewModel properties based on selectedTour
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

        private ObservableCollection<TourLogsModel> _tourLogs;

        // Property to hold the Tour Logs
        public ObservableCollection<TourLogsModel> TourLogs
        {
            get => _tourLogs;
            set
            {
                _tourLogs = value;
                OnPropertyChanged(nameof(TourLogs));
            }
        }

   
    }
}
