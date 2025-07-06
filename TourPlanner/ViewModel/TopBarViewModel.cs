using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using tour_planner.Commands;
using TourPlanner.DAL.Queries;

namespace tour_planner.ViewModel
{



    internal class TopBarViewModel
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CreateTourLogQuery));
        public ICommand Maximize { get; set; }
        public ICommand Minimize { get; set; }
        public ICommand Close { get; set; }
        private Window _window;

        public TopBarViewModel(Window window)
        {
            _window = window;
            Maximize = new RelayCommand(DoMaximize, CanExecuteWindowCommand);
            Minimize = new RelayCommand(DoMinimize, CanExecuteWindowCommand);
            Close = new RelayCommand(DoClose, CanExecuteWindowCommand);

        }

        private bool CanExecuteWindowCommand(object obj)
        {
            return _window != null;
        }

        public void DoMaximize(object obj)
        {
            if (_window.WindowState == WindowState.Maximized)
                _window.WindowState = WindowState.Normal;
            else
                _window.WindowState = WindowState.Maximized;
        }

        public void DoMinimize(object obj)
        {
            log.Info("User minimized application");
            _window.WindowState = WindowState.Minimized;

        }

        public void DoClose(object obj)
        {
            log.Info("User closed application");
            _window.Close();

        }
    }
}
