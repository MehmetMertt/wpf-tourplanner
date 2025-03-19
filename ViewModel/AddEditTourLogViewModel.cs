using System.Windows.Input;
using tour_planner.Commands;
using tour_planner.Model;

internal class AddEditTourLogViewModel
{
    private TourLogsModel _originalTourLog;

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

    public ICommand SaveCommandLog { get; set; }

    public AddEditTourLogViewModel(TourLogsModel selectedTourLog, bool isActionEnabled = true)
    {
        this.IsActionEnabled = isActionEnabled;

        // Store the original
        _originalTourLog = selectedTourLog;

        // Create a copy for editing
        EditableTourLog = new TourLogsModel(
            selectedTourLog.Date,
            selectedTourLog.Duration,
            selectedTourLog.Distance);

        SaveCommandLog = new RelayCommand(DoAddTour, CanAddTour);
    }

    private void DoAddTour(object obj)
    {
        // Only when saving, copy the edited values back to the original
        _originalTourLog.Date = EditableTourLog.Date;
        _originalTourLog.Duration = EditableTourLog.Duration;
        _originalTourLog.Distance = EditableTourLog.Distance;

        if (obj is System.Windows.Window window)
        {
            window.DialogResult = true;
            window.Close();
        }
    }

    private bool CanAddTour(object obj)
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