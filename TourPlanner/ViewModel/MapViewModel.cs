using System.ComponentModel;
using System.Windows.Input;
using tour_planner.Commands;
using TourPlanner.Domain;

public class MapViewModel : ViewModelBase
{
    public ICommand ShowRouteCommand { get; }

    public MapViewModel()
    {
        ShowRouteCommand = new RelayCommand(ShowRoute, (obj) => true);
    }

    private void ShowRoute(object obj)
    {
        if (obj is TourModel tour)
        {
            ShowRouteFromTour(tour);
        }
    }

    public void ResetMap()
    {
        MapResetRequested?.Invoke(this, EventArgs.Empty);
    }

    public void ShowRouteFromTour(TourModel tour)
    {
        ShowRouteRequested?.Invoke(this, new CitiesChangedEventArgs(tour.From, tour.To));
    }

    public event EventHandler MapResetRequested;
    public event EventHandler ShowRouteRequested;

}


public class CitiesChangedEventArgs : EventArgs
{
    public string From { get; }
    public string To { get; }

    public CitiesChangedEventArgs(string from, string to)
    {
        From = from;
        To = to;
    }
}
