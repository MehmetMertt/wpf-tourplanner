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
using System.Text.RegularExpressions;

namespace tour_planner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
/*            TourListViewModel routeViewModel = new TourListViewModel();
            this.DataContext = routeViewModel;*/
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


        private void lstRoutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            if (lstRoutes.SelectedItem is ListBoxItem selectedItem)
            {
                string selectedRoute = selectedItem.Content.ToString();
                UpdateRoute(selectedRoute);
            }
            */
        }
        /*
        private void UpdateRoute(string routeName)
        {
            List<Route> tourData = new List<Route>();
            mapView.Markers.Clear();

            switch (routeName)
            {
                case "Wien Tour":
                    mapView.Position = new PointLatLng(48.2082, 16.3738);
                    tourData.Add(new Route { Date = "24.02.2025", Duration = "2h", Distance = "15 km" });

                    AddMarker(48.2082, 16.3738, "Start - Stephansplatz");
                    AddMarker(48.2064, 16.3701, "Stop 1 - Karlskirche");
                    AddMarker(48.2013, 16.3622, "Stop 2 - Schloss Belvedere");
                    break;

                case "NÖ Tour":
                    mapView.Position = new PointLatLng(48.3031, 16.2476);
                    tourData.Add(new Route { Date = "25.02.2025", Duration = "3h", Distance = "25 km" });

                    AddMarker(48.3031, 16.2476, "Start - Klosterneuburg");
                    AddMarker(48.2976, 16.2214, "Stop 1 - Stift Heiligenkreuz");
                    AddMarker(48.2832, 16.1876, "Stop 2 - Mayerling");
                    break;

                case "Favoriten Tour":
                    mapView.Position = new PointLatLng(48.1702, 16.3635);
                    tourData.Add(new Route { Date = "26.02.2025", Duration = "1.5h", Distance = "10 km" });

                    AddMarker(48.1702, 16.3635, "Start - Hauptbahnhof");
                    AddMarker(48.1680, 16.3690, "Stop 1 - Böhmischer Prater");
                    break;

                case "Sestadt Tour":
                    mapView.Position = new PointLatLng(48.2653, 16.5116);
                    tourData.Add(new Route { Date = "27.02.2025", Duration = "2h", Distance = "12 km" });

                    AddMarker(48.2653, 16.5116, "Start - Seestadt Aspern");
                    AddMarker(48.2615, 16.5069, "Stop 1 - TechGate Vienna");
                    break;
            }

            datagrid.ItemsSource = tourData;
        }

        private void AddMarker(double lat, double lng, string description)
        {
            GMapMarker marker = new GMapMarker(new PointLatLng(lat, lng))
            {
                Shape = new System.Windows.Shapes.Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Stroke = System.Windows.Media.Brushes.Red,
                    Fill = System.Windows.Media.Brushes.Red
                }
            };
            mapView.Markers.Add(marker);
        }
        */
    }
}