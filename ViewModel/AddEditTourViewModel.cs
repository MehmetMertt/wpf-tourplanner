using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tour_planner.Commands;
using tour_planner.Model;
using tour_planner.View;

namespace tour_planner.ViewModel
{
    class AddEditTourViewModel
    {
        public TourModel Tour { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ToggleActionCommand { get; set; }

        public AddEditTourViewModel(TourModel tour, bool _IsActionEnabled = true)
        {
            Tour = tour;
            SaveCommand = new RelayCommand(DoAddTour, CanAddTour);
            ToggleActionCommand = new RelayCommand((object obj) => IsActionEnabled = !IsActionEnabled, (object obj) => true);
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
            if (obj is System.Windows.Window window)
            {
                window.DialogResult = true;
                window.Close();
            }

        }


        private bool CanAddTour(object obj)
        {
            if(Tour.TotalDistance > 0 && String.IsNullOrEmpty(Tour.TotalDuration) == false && String.IsNullOrEmpty(Tour.Name) == false && String.IsNullOrEmpty(Tour.Date) == false)
            {
                return true;
            }
            return false;
        }


    }
}
