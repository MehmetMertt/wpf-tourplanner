// In MapView.xaml.cs
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace tour_planner.View
{
    public partial class MapView : UserControl
    {
        private MapViewModel _viewModel;

        public MapView()
        {
            InitializeComponent();
            // We will get the ViewModel from the DataContext, so we hook events in the Loaded event.
            Loaded += MapView_Loaded;
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // 1. Get the ViewModel from the DataContext set by MainWindow.
            _viewModel = this.DataContext as MapViewModel;
            if (_viewModel == null)
            {
                Debug.WriteLine("CRITICAL: MapViewModel is null. DataContext was not set correctly.");
                return;
            }

            // 2. Subscribe to the event on the correct ViewModel instance.
            _viewModel.ShowRouteRequested += OnShowRouteRequested;
            _viewModel.MapResetRequested += OnMapResetRequested;

            // 3. Initialize the WebView2
            Debug.WriteLine("Loading map");
            await mapView.EnsureCoreWebView2Async(null);
            string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "map.html");

            // Make sure the file exists before trying to load it
            if (File.Exists(htmlPath))
            {
                mapView.Source = new Uri(htmlPath);
            }
            else
            {
                Debug.WriteLine($"CRITICAL: map.html not found at {htmlPath}");
                MessageBox.Show($"Error: map.html could not be found. Ensure its 'Copy to Output Directory' property is set to 'Copy if newer'.", "File Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void OnMapResetRequested(object sender, EventArgs e)
        {
            if (mapView == null || mapView.CoreWebView2 == null)
            {
                Debug.WriteLine("WebView not ready to execute script for reset.");
                return;
            }

            try
            {
                await mapView.ExecuteScriptAsync("resetMap()");
                Debug.WriteLine("Map reset command sent to JavaScript.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"JavaScript reset call failed: {ex.Message}");
            }
        }

        private async void OnShowRouteRequested(object sender, EventArgs e)
        {
            if (e is CitiesChangedEventArgs routeArgs)
            {
                if (mapView == null || mapView.CoreWebView2 == null)
                {
                    Debug.WriteLine("WebView not ready to execute script.");
                    return;
                }

                string js = $"showRouteFromCSharp('{routeArgs.From}', '{routeArgs.To}')";
                try
                {
                    string result = await mapView.ExecuteScriptAsync(js);
                    Debug.WriteLine($"JS result: {result}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"JavaScript call failed: {ex.Message}");
                }
            }
        }
    }
}