using log4net;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using tour_planner.Commands;
using tour_planner.Model;
using tour_planner.View;
using TourPlanner.BL.OpenRouteServiceAPI;
using TourPlanner.BL.OpenWeatherMapAPI;
using TourPlanner.DAL.Queries;
using TourPlanner.Domain;

namespace tour_planner.ViewModel
{


    class EditTourViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CreateTourLogQuery));
        public TourModel Tour { get; set; }
        public TourModel _copyTour { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand ToggleActionCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public string FromWeatherForecast { get; set; }
        public string ToWeatherForecast { get; set; }
        public string FromWeatherIcon { get; set; }
        public string ToWeatherIcon { get; set; }
        public string WeatherTip
        {
            get
            {
                string allForecasts = (FromWeatherForecast ?? "") + " " + (ToWeatherForecast ?? "");
                if (allForecasts.Contains("rain"))
                    return "Dont forget to take an umbrella 🌂";
                if (allForecasts.Contains("clear") && allForecasts.Contains("hot"))
                    return "Stay hydrated 💧";
                if (allForecasts.Contains("clear"))
                    return "Dont forget sun protection 🧴";
                if (allForecasts.Contains("snow"))
                    return "Dress warmly! ❄️";
                return "";
            }
        }
        public ITourManager _tourManager { get; }

        private MapView _mapViewControl;

        public EditTourViewModel(TourModel tour, ITourManager tourManager, MapView mapViewControl, bool _IsActionEnabled = true)
        {

            _mapViewControl = mapViewControl;
            _tourManager = tourManager;


            Tour = tour;
            _copyTour = new TourModel()
    .WithId(tour.Id)
    .WithName(tour.Name)
    .WithDate(tour.Date)
    .WithDuration(tour.TotalDuration)
    .WithDistance(tour.TotalDistance)
    .WithImagePath(tour.ImagePath)
    .WithDescription(tour.Description)
    .WithFrom(tour.From)
    .WithTo(tour.To)
    .WithTransportType(tour.TransportType)
    .WithTourLogs(new ObservableCollection<TourLogsModel>(tour.TourLogs));

            UpdateCommand = new RelayCommand(DoUpdateTour, CanUpdateTour);
            ToggleActionCommand = new RelayCommand((obj) => IsActionEnabled = !IsActionEnabled, (obj) => true);
            CancelCommand = new RelayCommand(DoCancel, CanCancel);
            IsActionEnabled = _IsActionEnabled;

            if (_copyTour != null)
            {
                _copyTour.PropertyChanged += async (s, e) =>
                {
                    if (e.PropertyName == nameof(_copyTour.From))
                        await UpdateFromWeatherAsync();
                    else if (e.PropertyName == nameof(_copyTour.To))
                        await UpdateToWeatherAsync();
                };
            }
        }



        private bool _isActionEnabled;
        public bool IsActionEnabled
        {
            get => _isActionEnabled;
            set
            {
                _isActionEnabled = value;
            }
        }



        private async void DoUpdateTour(object obj)
        {
            try
            {
                if (obj is TourModel)
                {
                    log.Info($"User tries to update Tour with {obj}");
                }
                // Copy values from backup

                Tour.Description = _copyTour.Description;
                Tour.To = _copyTour.To;
                Tour.From = _copyTour.From;
                Tour.TransportType = _copyTour.TransportType;
                Tour.ImagePath = _copyTour.ImagePath;
                Tour.Name = _copyTour.Name;
                Tour.Date = _copyTour.Date;

                OpenRouteServiceClient osc = OpenRouteServiceClient.Instanz;

                (float distance, float duration) = await osc.GetTimeDistance(VehicleProfile.DrivingCar, Tour);

                Tour.TotalDistance = distance;
                Tour.TotalDuration = duration;

                string filename = $"{Tour.Name.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}";
                ((MapViewModel)_mapViewControl.DataContext).ShowRouteFromTour(Tour);
                await Task.Delay(5000); // webview could not take any longer to load the map :P
                Tour.ImagePath = await _mapViewControl.SaveMapScreenshotAsync(filename);

                if (obj is System.Windows.Window window)
                {
                    _tourManager.UpdateTour(Tour);
                    window.DialogResult = true;
                    window.Close();
                }
                log.Info($"Tour {Tour.Name} successfully updates");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to update the tour. Reason: " + ex.Message,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }


        private bool CanUpdateTour(object obj)
        {
            return !_copyTour.HasErrors;
        }

        private void DoCancel(object obj)
        {
            if (obj is System.Windows.Window window)
            {
                window.DialogResult = false;
                window.Close();
            }
        }

        private bool CanCancel(object obj) => true;

        private async Task UpdateFromWeatherAsync()
        {
            var client = OpenWeatherServiceClient.Instance;
            var summary = await client.GetForecastSummary(_copyTour.From, _copyTour.Date);
            FromWeatherForecast = summary;
            FromWeatherIcon = ChooseWeatherIcon(summary);
            OnPropertyChanged(nameof(FromWeatherForecast));
            OnPropertyChanged(nameof(FromWeatherIcon));
            OnPropertyChanged(nameof(WeatherTip));
        }

        private async Task UpdateToWeatherAsync()
        {
            var client = OpenWeatherServiceClient.Instance;
            var summary = await client.GetForecastSummary(_copyTour.To, _copyTour.Date);
            ToWeatherForecast = summary;
            ToWeatherIcon = ChooseWeatherIcon(summary);
            OnPropertyChanged(nameof(ToWeatherForecast));
            OnPropertyChanged(nameof(ToWeatherIcon));
            OnPropertyChanged(nameof(WeatherTip));
        }

        internal string ChooseWeatherIcon(string summary)
        {
            if (summary.Contains("rain")) return "🌧️";
            if (summary.Contains("snow")) return "❄️";
            if (summary.Contains("clear")) return "☀️";
            if (summary.Contains("cloud")) return "☁️";
            return "🌡️";
        }
    }
}
