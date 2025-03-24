using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using tour_planner.ViewModel;
using tour_planner.View;

using System.Text.RegularExpressions;
using tour_planner.Model;
using System.Diagnostics;

namespace tour_planner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TourListViewModel tourListViewModel;
        private TourLogsViewModel tourLogsViewModel;

        public MainWindow()
        {
            InitializeComponent();


            TourListViewModel routeViewModel = new TourListViewModel();
            RoutesView.DataContext = routeViewModel;

            TourLogsViewModel tourLogsViewModel = new TourLogsViewModel(routeViewModel);
            TourLogsView.DataContext = tourLogsViewModel;




        }

      

        // https://stackoverflow.com/questions/13930633/in-wpf-can-i-have-a-borderless-window-that-has-regular-minimize-maximise-and-c
        #region ControlButtons
        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void CommandBinding_Executed_2(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void CommandBinding_Executed_3(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void DragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove(); // Enables window dragging
        }
        #endregion

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            mapView.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            mapView.MinZoom = 12;
            mapView.MaxZoom = 17;
            mapView.Zoom = 12;
            mapView.Position = new PointLatLng(48.239166, 16.377441); //technikum wien
            mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            mapView.CanDragMap = true;
            mapView.DragButton = MouseButton.Left;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }


    
    }
}