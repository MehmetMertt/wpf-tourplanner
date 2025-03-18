using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using tour_planner.Commands;

namespace tour_planner.ViewModel
{



    internal class TopBarViewModel
    {

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
            Debug.WriteLine("Maximize");
            Debug.WriteLine("Maximize");

            Debug.WriteLine("Maximize");

            if (_window.WindowState == WindowState.Maximized)
                _window.WindowState = WindowState.Normal;
            else
                _window.WindowState = WindowState.Maximized;
         
        }

        public void DoMinimize(object obj)
        {
            _window.WindowState = WindowState.Minimized;

        }

        public void DoClose(object obj)
        {
            _window.Close();

        }
    }
}
