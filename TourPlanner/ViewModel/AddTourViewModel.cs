﻿using log4net;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using tour_planner.Commands;
using tour_planner.Model;
using tour_planner.View;
using TourPlanner.BL.OpenRouteServiceAPI;
using TourPlanner.Domain;
using TourPlanner.BL.OpenWeatherMapAPI;
using System.Collections.ObjectModel;

namespace tour_planner.ViewModel
{
    class AddTourViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RouteRequest));

        public TourModel Tour { get; set; }
        private TourModel _copyTour { get; set; }
        public TourModel CopyTour {
            get => _copyTour;
            set
            {
                if (_copyTour != null)
                {
                }
                _copyTour = value;
                if (_copyTour != null)
                {
                }
                OnPropertyChanged(nameof(CopyTour));
            }

        }


        public ICommand SaveCommand { get; set; }
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

        private readonly MapView _mapViewControl;

        public AddTourViewModel(TourModel tour, ITourManager tourManager, MapView mapViewControl, bool _IsActionEnabled = true)
        {
            _mapViewControl = mapViewControl;

            // Create a copy for editing

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

            SaveCommand = new RelayCommand(DoAddTour, CanAddTour);
            ToggleActionCommand = new RelayCommand((obj) => IsActionEnabled = !IsActionEnabled, (obj) => true);
            CancelCommand = new RelayCommand(DoCancel, CanCancel);
            IsActionEnabled = _IsActionEnabled;

            if (_copyTour != null)
            {
                _copyTour.PropertyChanged += async (s, e) =>
                {
                    if (e.PropertyName == nameof(CopyTour.From))
                        await UpdateFromWeatherAsync();
                    else if (e.PropertyName == nameof(CopyTour.To))
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



        private async void DoAddTour(object obj)
        {

            try
            {
                log.Info("User trying to add tour");
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
                    _tourManager.AddTour(Tour);
                    window.DialogResult = true;
                    window.Close();
                }
                log.Info("User added Tour successfully");
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Failed to add the tour. Reason: " + e.Message,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                log.Warn($"Adding Tour failed: {e}");

            }
        }



        private bool CanAddTour(object obj)
        {
            return !CopyTour.HasErrors;
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
            var summary = await client.GetForecastSummary(CopyTour.From, CopyTour.Date);
            FromWeatherForecast = summary;
            FromWeatherIcon = ChooseWeatherIcon(summary);
            OnPropertyChanged(nameof(FromWeatherForecast));
            OnPropertyChanged(nameof(FromWeatherIcon));
            OnPropertyChanged(nameof(WeatherTip));
        }

        private async Task UpdateToWeatherAsync()
        {
            var client = OpenWeatherServiceClient.Instance;
            var summary = await client.GetForecastSummary(CopyTour.To, CopyTour.Date);
            ToWeatherForecast = summary;
            ToWeatherIcon = ChooseWeatherIcon(summary);
            OnPropertyChanged(nameof(ToWeatherForecast));
            OnPropertyChanged(nameof(ToWeatherIcon));
            OnPropertyChanged(nameof(WeatherTip));
        }

        private string ChooseWeatherIcon(string summary)
        {
            if (summary.Contains("rain")) return "🌧️";
            if (summary.Contains("snow")) return "❄️";
            if (summary.Contains("clear")) return "☀️";
            if (summary.Contains("cloud")) return "☁️";
            return "🌡️";
        }
    }
}
