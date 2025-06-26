using log4net;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using tour_planner.Commands;
using tour_planner.Model;
using TourPlanner.BL.OpenRouteServiceAPI;
using TourPlanner.DAL.Queries;
using TourPlanner.Domain;

namespace tour_planner.ViewModel
{


    class EditTourViewModel
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CreateTourLogQuery));
        public TourModel Tour { get; set; }
        public TourModel _copyTour { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand ToggleActionCommand { get; set; }
        public TourManager _tourManager { get; }

        public EditTourViewModel(TourModel tour, TourManager tourManager, bool _IsActionEnabled = true)
        {


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
                if(obj is TourModel)
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

       

    }
}
