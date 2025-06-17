using System;
using System.Windows.Input;
using tour_planner.Commands;
using TourPlanner.Domain;
using TourPlanner.Model; 

namespace tour_planner.ViewModel
{
   
    public class AddTourLogViewModel : ViewModelBase 
    {
        public TourLogsManager _tourLogsManager { get; }

        private TourLogsModel _editableTourLog; 
        public TourLogsModel EditableTourLog
        {
            get => _editableTourLog;
            set
            {
                if (_editableTourLog == value) return;
                _editableTourLog = value;
                OnPropertyChanged(nameof(EditableTourLog));
            }
        }

        private bool _isActionEnabled;
        public bool IsActionEnabled
        {
            get => _isActionEnabled;
            set
            {
                if (_isActionEnabled == value) return;
                _isActionEnabled = value;
                OnPropertyChanged(nameof(IsActionEnabled));
            }
        }

        public ICommand SaveCommandLog { get; set; }
        public ICommand CancelCommandLog { get; set; }


        public AddTourLogViewModel(TourLogsModel newLog, TourLogsManager tourLogsManager)
        {
            _tourLogsManager = tourLogsManager;

            EditableTourLog = newLog;

           
            SaveCommandLog = new RelayCommand(DoAddTour, CanAddTour);
            CancelCommandLog = new RelayCommand(DoCancelTourLog,(obj) => true); 

            IsActionEnabled = true;
        }

        private void DoAddTour(object obj)
        {
     

            if (obj is System.Windows.Window window)
            {
                
                _tourLogsManager.AddLogToTour(EditableTourLog.TourId, EditableTourLog);
                window.DialogResult = true; 
                window.Close();
            }
        }

        private bool CanAddTour(object obj)
        {
   
            return !EditableTourLog.HasErrors;
        }

        private void DoCancelTourLog(object obj)
        {
            if (obj is System.Windows.Window window)
            {
                window.DialogResult = false; 
                window.Close();
            }
        }
    }
}