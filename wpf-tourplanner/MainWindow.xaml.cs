using System.Windows;
using System.Windows.Input;
using tour_planner.ViewModel;
using tour_planner.View;
using tour_planner.Model;
using TourPlanner.DAL;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using TourPlanner.DAL.Queries.Tour;
using TourPlanner.DAL.Queries;
using TourPlanner.Model;
using TourPlanner.BL;
using TourPlanner.BL.ImportExport;

namespace tour_planner
{
    public partial class MainWindow : Window
    {
        private TourListViewModel tourListViewModel;
        private TourLogsViewModel tourLogsViewModel;
        public MapViewModel mapViewModel { get; set; }


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
            InitializeAsync();

            string host = AppSettingsManager.GetSetting("host");
            string username = AppSettingsManager.GetSetting("username");
            string password = AppSettingsManager.GetSetting("password");
            string database = AppSettingsManager.GetSetting("database");

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

            mapViewModel = new MapViewModel();

            tourListViewModel = new TourListViewModel(_tourManager,_tourExport, _tourImport, this);
            RoutesView.DataContext = tourListViewModel;

            tourLogsViewModel = new TourLogsViewModel(tourListViewModel, _tourLogsManager);
            TourLogsView.DataContext = tourLogsViewModel;

            MapViewControl.DataContext = mapViewModel;

            tourListViewModel.OnTourSelected += (s, tour) =>
            {
                if (tour != null)
                    mapViewModel.ShowRouteFromTour(tour);
            };

            tourListViewModel.TourDeselected += (s, e) =>
            {
                mapViewModel.ResetMap();
            };

            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community; // to be able to use questpdf for report generation

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

        private async void ShowRouteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void DragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove(); // Enables window dragging

        }
        #endregion


        private async void InitializeAsync()
        {
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        public async Task<string> CaptureMapScreenshotAsync(string filename)
        {
            return await MapViewControl.SaveMapScreenshotAsync(filename);
        }

    }
}