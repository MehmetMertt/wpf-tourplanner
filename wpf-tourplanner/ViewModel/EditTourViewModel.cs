using System.Globalization;
using System.Windows;
using System.Windows.Input;
using tour_planner.Commands;
using tour_planner.Model;
using tour_planner.View;
using TourPlanner.BL.OpenRouteServiceAPI;
using TourPlanner.Domain;

namespace tour_planner.ViewModel
{
    class EditTourViewModel
    {
        public TourModel Tour { get; set; }
        public TourModel _copyTour { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand ToggleActionCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public TourManager _tourManager { get; }

        private MapView _mapViewControl;

        public EditTourViewModel(TourModel tour, TourManager tourManager, MapView mapViewControl, bool _IsActionEnabled = true)
        {

            _mapViewControl = mapViewControl;
            _tourManager = tourManager;


            Tour = tour;
            _copyTour = new TourModel(
                tour.Id,
                tour.Name,
                tour.Date,
            tour.TotalDuration,
            tour.TotalDistance,
            tour.ImagePath,
            tour.Description,
            tour.From,
            tour.To,
            tour.TransportType,
            tour.TourLogs
            );
            UpdateCommand = new RelayCommand(DoUpdateTour, CanUpdateTour);
            ToggleActionCommand = new RelayCommand((obj) => IsActionEnabled = !IsActionEnabled, (obj) => true);
            CancelCommand = new RelayCommand(DoCancel, CanCancel);
            IsActionEnabled = _IsActionEnabled;
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
    }
}
