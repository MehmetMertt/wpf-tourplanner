using log4net;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using tour_planner.Commands;
using tour_planner.Model;
using tour_planner.View;
using TourPlanner.BL.ImportExport;
using TourPlanner.Domain;


namespace tour_planner.ViewModel
{
    public class TourListViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TourListViewModel));

        public ObservableCollection<TourModel> Tours { get; set; } = new ObservableCollection<TourModel>();

        public ICommand ShowWindowCommand { get; set; }

        private TourManager _tourManager { get; set; }

        private ITourExportService _tourExportService { get; set; }

        public ITourImportService _tourImportService { get; set; }

        private readonly MapView _mapViewControl;
        public TourListViewModel(TourManager tourManager,ITourExportService exportService,ITourImportService importService, MapView mapViewControl)
        {
            _mapViewControl = mapViewControl;
            _tourManager = tourManager;
            DeleteTourCommand = new RelayCommand(DoDeleteTour, CanDeleteTour);
            OpenAddPage = new RelayCommand(OpenAddTour, CanOpenAddTour);
            OpenEditPage = new RelayCommand(OpenEditTour, CanOpenEditTour);
            OpenDetailsPage = new RelayCommand(OpenViewPage, CanOpenViewPage);
            DoImport = new RelayCommand(OpenImport,(obj) => true);
            DoExport = new RelayCommand(OpenExport, CanExport);
            _tourExportService = exportService;
            _tourImportService = importService;

            // set the Filter
            var view = CollectionViewSource.GetDefaultView(Tours);
            view.Filter = TourFilter;

            LoadTours();
        }

        private bool CanExport(object obj)
        {
           if(_selectedTour != null)
           {
                return true;
           } 
           return false;
            
        }
        private async void OpenExport(object obj)
        {
            log.Info("User tries to export Tour");
            if (_selectedTour == null)
            {
                log.Info("Exporting failed because no Tour was selected");
                return;
            }

            try
            {
                await _tourExportService.ExportTourAsync(_selectedTour);
                MessageBox.Show("Export successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export failed:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenImport(object obj)
        {
            log.Info("User tries to import Logs");
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Tour Log files (*.tourlog)|*.tourlog",
                Title = "Import Tour Logs"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;
                log.Info($"User selected file: {filePath} ");
                var importedTour = _tourImportService.ImportToursAsync(filePath).Result;

                    var newTourId = Guid.NewGuid();
                    importedTour.Id = newTourId;
                    foreach(var log in importedTour.TourLogs)
                    {
                        log.Id = Guid.NewGuid();
                        log.TourId = newTourId;
                    }
               

                Tours.Add(importedTour);
                _tourManager.AddTour(importedTour);
                log.Warn("Importing Logs successfully");


            }
        }
    

        public ICommand AddTourCommand { get; set; } // command to link button to a function to add a new tour
        public ICommand DeleteTourCommand { get; set; } // command to link button to a function to delete an existing route 
        public ICommand OpenAddPage { get; set; }
        public ICommand DoImport { get; set; }
        public ICommand DoExport { get; set; }
        public ICommand OpenEditPage { get; set; }
        public ICommand OpenDetailsPage { get; set; }

        public event EventHandler<TourModel> OnTourSelected;
        public event EventHandler TourDeselected;

        private TourModel _selectedTour;
        public virtual TourModel SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour)); // to update entries in data grid
                if (_selectedTour != null)
                {
                    TourDeselected?.Invoke(this, EventArgs.Empty);
                    OnTourSelected?.Invoke(this, _selectedTour);
                }
                else
                {
                    TourDeselected?.Invoke(this, EventArgs.Empty);
                }

            }


        }



        private void OpenViewPage(object obj)
        {
            var dialog = new EditTourView
            {
                DataContext = new EditTourViewModel(_selectedTour, _tourManager, _mapViewControl ,false)
            };

            dialog.ShowDialog();
        }


        private bool CanOpenViewPage(object obj)
        {
            return true;
        }


        private bool CanOpenEditTour(object obj)
        {
            if (_selectedTour is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void OpenEditTour(object obj)
        {
            var dialog = new EditTourView
            {
                DataContext = new EditTourViewModel(_selectedTour, _tourManager, _mapViewControl)
            };

            if (dialog.ShowDialog() == true)
            {
                LoadTours();
            }
        }

        private bool CanOpenAddTour(object obj)
        {
            return true;
        }

        private void OpenAddTour(object obj)
        {
            TourModel newTour = new TourModel(Guid.NewGuid(), "Name", "DD.MM.YYYY", 0f, 0f, "", "", "", "", "",new());
            var dialog = new AddTourView()
            {
                DataContext = new AddTourViewModel(newTour, _tourManager, _mapViewControl)
            };
            if (dialog.ShowDialog() == true)
            {
                Tours.Add(newTour);
            }
        }




        private void DoDeleteTour(object obj)
        {

            _tourManager.DeleteTour(_selectedTour.Id);
            var tourToRemove = Tours.FirstOrDefault(y => y.Id == _selectedTour.Id);
            if (tourToRemove != null)
            {
                Tours.Remove(tourToRemove);
            }
        }

        private bool CanDeleteTour(object obj)
        {
            if (SelectedTour != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        private async void LoadTours()
        {

            var tours = await _tourManager.getTours();
            Tours.Clear();
            foreach (var tour in tours)
            {
                Debug.Write($"{tour.Name} has {tour.TourLogs.Count} Logs");
                Tours.Add(tour);
            }
            CollectionViewSource.GetDefaultView(Tours).Refresh();
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                CollectionViewSource.GetDefaultView(Tours)?.Refresh(); // update filter
            }
        }

        private bool TourFilter(object item)
        {
            if (item is not TourModel tour) return false;
            if (string.IsNullOrWhiteSpace(SearchText)) return true;

            var search = SearchText.Trim().ToLower();

            bool matchesTour = (tour.Name?.ToLower().Contains(search) ?? false)               // filter by tour attributes
                || (tour.Date?.ToLower().Contains(search) ?? false)
                || tour.TotalDuration.ToString(CultureInfo.InvariantCulture).Contains(search)
                || tour.TotalDistance.ToString(CultureInfo.InvariantCulture).Contains(search)
                || (tour.Description?.ToLower().Contains(search) ?? false)
                || (tour.From?.ToLower().Contains(search) ?? false)
                || (tour.To?.ToLower().Contains(search) ?? false)
                || (tour.TransportType?.ToLower().Contains(search) ?? false);

            bool matchesLog = tour.TourLogs.Any(log =>                                  // filter by tourlog attributes
                (log.Comment?.ToLower().Contains(search) ?? false)
                || (log.Difficulty?.ToLower().Contains(search) ?? false)
                || log.Rating.ToString().Contains(search)
                || log.Distance.ToString(CultureInfo.InvariantCulture).Contains(search)
                || log.Duration.ToString(CultureInfo.InvariantCulture).Contains(search)
                || log.Date.ToString("dd.MM.yyyy").Contains(search)
            );

            return matchesTour || matchesLog;
        }
    }
}
