using System;
using System.Windows.Input;
using tour_planner.Commands;
using tour_planner.Model;
using TourPlanner.Domain;
using TourPlanner.Model; 

namespace tour_planner.ViewModel
{
    public class EditTourLogViewModel : ViewModelBase
    {
        private TourLogsModel _originalTourLog;

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

        public ICommand UpdateCommandLog { get; set; }
        public ICommand CancelCommandLog { get; set; }


        public EditTourLogViewModel(TourLogsModel selectedTourLog, TourLogsManager tourLogsManager)
        {
            _tourLogsManager = tourLogsManager;

            _originalTourLog = selectedTourLog;

            EditableTourLog = new TourLogsModel(
                selectedTourLog.Id,
                selectedTourLog.Date,
                selectedTourLog.Duration,
                selectedTourLog.Distance,
                selectedTourLog.Comment,
                selectedTourLog.Difficulty,
                selectedTourLog.Rating,
                selectedTourLog.TourId
            );
           
            EditableTourLog.DateString = selectedTourLog.Date.ToString("dd.MM.yyyy");
            EditableTourLog.DurationString = selectedTourLog.Duration.ToString();
            EditableTourLog.DistanceString = selectedTourLog.Distance.ToString();


            UpdateCommandLog = new RelayCommand(DoUpdateTour, CanUpdateTour);
            CancelCommandLog = new RelayCommand(DoCancelTourLog, (obj) => true);

            IsActionEnabled = true; 
        }

        private void DoUpdateTour(object obj)
        {
            if (obj is System.Windows.Window window)
            {
           
                _originalTourLog.Date = EditableTourLog.Date;
                _originalTourLog.Duration = EditableTourLog.Duration;
                _originalTourLog.Distance = EditableTourLog.Distance;
                _originalTourLog.Comment = EditableTourLog.Comment;
                _originalTourLog.Difficulty = EditableTourLog.Difficulty;
                _originalTourLog.Rating = EditableTourLog.Rating;

                _tourLogsManager.UpdateLog(_originalTourLog);
                window.DialogResult = true;
                window.Close();
            }
        }

        private bool CanUpdateTour(object obj)
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