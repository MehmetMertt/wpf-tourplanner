﻿using System.Text;
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
        }


        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            mapView.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            mapView.MinZoom = 2;
            mapView.MaxZoom = 17;
            mapView.Position = new PointLatLng(48.239166, 16.377441);
            mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            mapView.CanDragMap = true;
            mapView.DragButton = MouseButton.Left;
        }


        private void lstRoutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstRoutes.SelectedItem is ListBoxItem selectedItem)
            {
                string selectedRoute = selectedItem.Content.ToString();
                UpdateRoute(selectedRoute);
            }
        }

        private void UpdateRoute(string routeName)
        {
            List<TourData> tourData = new List<TourData>();
            mapView.Markers.Clear();

            switch (routeName)
            {
                case "Wien Tour":
                    mapView.Position = new PointLatLng(48.2082, 16.3738);
                    tourData.Add(new TourData { Date = "24.02.2025", Duration = "2h", Distance = "15 km" });

                    AddMarker(48.2082, 16.3738, "Start - Stephansplatz");
                    AddMarker(48.2064, 16.3701, "Stop 1 - Karlskirche");
                    AddMarker(48.2013, 16.3622, "Stop 2 - Schloss Belvedere");
                    break;

                case "NÖ Tour":
                    mapView.Position = new PointLatLng(48.3031, 16.2476);
                    tourData.Add(new TourData { Date = "25.02.2025", Duration = "3h", Distance = "25 km" });

                    AddMarker(48.3031, 16.2476, "Start - Klosterneuburg");
                    AddMarker(48.2976, 16.2214, "Stop 1 - Stift Heiligenkreuz");
                    AddMarker(48.2832, 16.1876, "Stop 2 - Mayerling");
                    break;

                case "Favoriten Tour":
                    mapView.Position = new PointLatLng(48.1702, 16.3635);
                    tourData.Add(new TourData { Date = "26.02.2025", Duration = "1.5h", Distance = "10 km" });

                    AddMarker(48.1702, 16.3635, "Start - Hauptbahnhof");
                    AddMarker(48.1680, 16.3690, "Stop 1 - Böhmischer Prater");
                    break;

                case "Sestadt Tour":
                    mapView.Position = new PointLatLng(48.2653, 16.5116);
                    tourData.Add(new TourData { Date = "27.02.2025", Duration = "2h", Distance = "12 km" });

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

    }

    public class TourData
    {
        public string Date { get; set; }
        public string Duration { get; set; }
        public string Distance { get; set; }
    }
}