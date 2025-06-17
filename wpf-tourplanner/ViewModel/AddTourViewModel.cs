using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using tour_planner.Commands;
using tour_planner.Model;
using TourPlanner.Domain;

namespace tour_planner.ViewModel
{
    class AddTourViewModel : ViewModelBase
    {
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
        public TourManager _tourManager { get; }



        public AddTourViewModel(TourModel tour, TourManager tourManager, bool _IsActionEnabled = true)
        {

            // Create a copy for editing

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
            tour.TransportType
            );
            SaveCommand = new RelayCommand(DoAddTour, CanAddTour);
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



        private void DoAddTour(object obj)
        {
            Tour.Description = _copyTour.Description;
            Tour.To = _copyTour.To;
            Tour.From = _copyTour.From;
            Tour.TransportType = _copyTour.TransportType;
            Tour.ImagePath = _copyTour.ImagePath;
            Tour.Name = _copyTour.Name;
            Tour.Date = _copyTour.Date;
            Tour.TotalDistance = _copyTour.TotalDistance;
            Tour.TotalDuration = _copyTour.TotalDuration;



            if (obj is System.Windows.Window window)
            {
                _tourManager.AddTour(Tour);
                window.DialogResult = true;
                window.Close();
            }

        }


        private bool CanAddTour(object obj)
        {
            return !CopyTour.HasErrors;
        }

  

    }
}
