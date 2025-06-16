using System.Windows.Input;
using tour_planner.Commands;
using tour_planner.Model;
using TourPlanner.Domain;
using TourPlanner.Model;

internal class EditTourLogViewModel
{
    private TourLogsModel _originalTourLog;

    public TourLogsManager _tourLogsManager { get; }


    public TourLogsModel EditableTourLog { get; set; }

    private bool _isActionEnabled;
    public bool IsActionEnabled
    {
        get => _isActionEnabled;
        set
        {
            _isActionEnabled = value;
        }
    }

    public ICommand UpdateCommandLog { get; set; }

    public EditTourLogViewModel(TourLogsModel selectedTourLog, TourLogsManager tourLogsManager, bool isActionEnabled = true)
    {
        this.IsActionEnabled = isActionEnabled;

        _tourLogsManager = tourLogsManager;

        // Store the original
        _originalTourLog = selectedTourLog;

        // Create a copy for editing
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

        UpdateCommandLog = new RelayCommand(DoUpdateTour, CanUpdateTour);
    }

    private void DoUpdateTour(object obj)
    {

        // Only when saving, copy the edited values back to the original
        _originalTourLog.Date = EditableTourLog.Date;
        _originalTourLog.Duration = EditableTourLog.Duration;
        _originalTourLog.Distance = EditableTourLog.Distance;
        _originalTourLog.Comment = EditableTourLog.Comment;
        _originalTourLog.Difficulty = EditableTourLog.Difficulty;
        _originalTourLog.Rating = EditableTourLog.Rating;


        if (obj is System.Windows.Window window)
        {


            _tourLogsManager.UpdateLog(_originalTourLog);
            window.DialogResult = true;
            window.Close();
        }
    }

    private bool CanUpdateTour(object obj)
    {
        if (EditableTourLog.Distance > 0 && EditableTourLog.Duration > 0 && IsValidDate(EditableTourLog.Date))
        {
            return true;
        }
        return false;
    }

    private bool IsValidDate(DateTime date)
    {
        return date != DateTime.MinValue && date != DateTime.MaxValue;
    }
}