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
using TourPlanner.DAL;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using System.Configuration;
using TourPlanner.DAL.Queries.Tour;
using TourPlanner.DAL.Queries;
using TourPlanner.Model;
using TourPlanner.BL;

namespace tour_planner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    /* TODO:
     * changable style/theme maybe

     */


    public partial class MainWindow : Window
    {
        private TourListViewModel tourListViewModel;
        private TourLogsViewModel tourLogsViewModel;

        private readonly TourDbContextFactory _tourDbContextFactory;
        public CreateTourQuery _createTourQuery;
        public DeleteTourQuery _deleteTourQuery;
        public UpdateTourQuery _updateTourQuery;
        public GetAllToursQuery _getAllToursQuery;
        public TourManager _tourManager;


        public CreateTourLogQuery _createTourLogQuery;
        public DeleteTourLogQuery _deleteTourLogQuery;
        public UpdateTourLogQuery _updateTourLogQuery;
        public GetAllToursLogQuery _getAllToursLogQuery;
        public GetTourLogsByTourIdQuery _getTourLogsByTourIdQuery;
        public TourLogsManager _tourLogsManager;

        public MainWindow()
        {
            InitializeComponent();

            string host = ConfigurationManager.AppSettings["host"];
            string username = ConfigurationManager.AppSettings["username"];
            string password = ConfigurationManager.AppSettings["password"];
            string database = ConfigurationManager.AppSettings["database"];

            if (String.IsNullOrEmpty(host) || String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(database))
            {
                throw new Exception("Please fill in the configuration file");
            }

            string connectionString = $"Host={host};Username={username};Password={password};Database={database}";


            _tourDbContextFactory = new TourDbContextFactory(
                new DbContextOptionsBuilder().UseNpgsql(connectionString).Options
                );

            using(TourDbContext context = _tourDbContextFactory.Create()) //good for future migrations -> auto migrations
            {
                context.Database.Migrate();
            }


            _createTourQuery = new CreateTourQuery(_tourDbContextFactory);
            _deleteTourQuery = new DeleteTourQuery(_tourDbContextFactory);
            _updateTourQuery = new UpdateTourQuery(_tourDbContextFactory);
            _getAllToursQuery = new GetAllToursQuery(_tourDbContextFactory);
            _tourManager = new TourManager(_createTourQuery, _deleteTourQuery, _updateTourQuery, _getAllToursQuery);



            _createTourLogQuery = new CreateTourLogQuery(_tourDbContextFactory);
            _deleteTourLogQuery = new DeleteTourLogQuery(_tourDbContextFactory);
            _updateTourLogQuery = new UpdateTourLogQuery(_tourDbContextFactory);
            _getAllToursLogQuery = new GetAllToursLogQuery(_tourDbContextFactory);
            _getTourLogsByTourIdQuery = new GetTourLogsByTourIdQuery(_tourDbContextFactory);

            _tourLogsManager = new TourLogsManager(_createTourLogQuery, _deleteTourLogQuery, _updateTourLogQuery, _getTourLogsByTourIdQuery);


            ITourExportService _tourExport = new TourExportService();
            ITourImportService _tourImport = new TourImportService();


            TourListViewModel routeViewModel = new TourListViewModel(_tourManager,_tourExport, _tourImport);
            RoutesView.DataContext = routeViewModel;

            TourLogsViewModel tourLogsViewModel = new TourLogsViewModel(routeViewModel,_tourLogsManager);
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