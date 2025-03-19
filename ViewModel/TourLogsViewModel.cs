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
        public TourLogsViewModel()
        {
            TourLogs = new ObservableCollection<TourLogsModel>();
            
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

        private void MainViewModel_OnTourSelected(object sender, TourModel selectedTour)
        {
            // Handle the selected tour here
            Debug.WriteLine($"Selected tour changed to: {selectedTour.Name}");
            // Update properties or trigger UI changes as needed
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
