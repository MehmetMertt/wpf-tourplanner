using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using tour_planner.Model;
using tour_planner.ViewModel;
using TourPlanner.BL.ImportExport;
using TourPlanner.Domain;
using TourPlanner.Model;

namespace TourPlannerUnitTests
{
    [TestFixture]
    public class TourLogsViewModelTests
    {
        private TourLogsViewModel _viewModel;
        private Mock<ITourLogsManager> _mockTourLogsManager;

        // We need a real TourListViewModel because TourLogsViewModel depends on its concrete implementation
        private TourListViewModel _tourListViewModel;
        private Mock<ITourManager> _mockTourManager; // Dependency for TourListViewModel

        [SetUp]
        public void Setup()
        {
            _mockTourLogsManager = new Mock<ITourLogsManager>();
            _mockTourManager = new Mock<ITourManager>();

      
            _tourListViewModel = new TourListViewModel(_mockTourManager.Object, null, null, null);

            _viewModel = new TourLogsViewModel(_tourListViewModel, _mockTourLogsManager.Object);
        }

/*        [Test]
        public async Task HandleTourSelected_WhenTourIsSelected_ShouldLoadLogsForThatTour()
        {
            var tourId = Guid.NewGuid();
            var selectedTour = new TourModel(tourId, "Test Tour", "01.01.2025", 10, 120, "image.png", "A test tour", "Vienna", "Salzburg", "Car", new());

            var logsToLoad = new List<TourLogsModel>
            {
                new TourLogsModel(Guid.NewGuid(), DateTime.Now, 1, 30, "Log 1", "Easy", 5, tourId),
                new TourLogsModel(Guid.NewGuid(), DateTime.Now, 2, 60, "Log 2", "Medium", 4, tourId)
            };

            _mockTourLogsManager.Setup(m => m.GetLogsForTour(tourId)).ReturnsAsync(logsToLoad);

            _viewModel.HandleTourSelected(this, selectedTour);
            await Task.Delay(50); // Allow async operations to complete

            Assert.That(_viewModel.TourLogs.Count, Is.EqualTo(2));
            Assert.That(_viewModel.TourLogs.First().Comment, Is.EqualTo("Log 1"));
            _mockTourLogsManager.Verify(m => m.GetLogsForTour(tourId), Times.Once);
        }*/

        [Test]
        public void CanOpenNewPage_WhenTourIsSelected_ShouldReturnTrue()
        {
            _tourListViewModel.SelectedTour = new TourModel()
                .WithId(Guid.NewGuid())
                .WithName("Sample Tour")
                .WithDate("01.01.2025")
                .WithDuration(0f)
                .WithDistance(0f)
                .WithImagePath("")
                .WithDescription("")
                .WithFrom("")
                .WithTo("")
                .WithTransportType("")
                .WithTourLogs(new ObservableCollection<TourLogsModel>());

            bool canExecute = _viewModel.OpenNewPage.CanExecute(null);

            Assert.IsTrue(canExecute);
        }


        [Test]
        public void CanOpenNewPage_WhenNoTourIsSelected_ShouldReturnFalse()
        {
            _tourListViewModel.SelectedTour = null;

            bool canExecute = _viewModel.OpenNewPage.CanExecute(null);

            Assert.IsFalse(canExecute);
        }

        [Test]
        public void CanOpenEditPage_WhenLogIsSelected_ShouldReturnTrue()
        {
            _viewModel.SelectedLog = new TourLogsModel(Guid.NewGuid(), DateTime.Now, 0, 0, "", "", 0, Guid.NewGuid());

            bool canExecute = _viewModel.OpenEditPage.CanExecute(null);

            Assert.IsTrue(canExecute);
        }

        [Test]
        public void CanOpenEditPage_WhenNoLogIsSelected_ShouldReturnFalse()
        {
            _viewModel.SelectedLog = null;

            bool canExecute = _viewModel.OpenEditPage.CanExecute(null);

            Assert.IsFalse(canExecute);
        }

        [Test]
        public void DeleteCommand_WhenExecutedWithSelectedLog_ShouldRemoveLogAndCallManager()
        {
            var logId = Guid.NewGuid();
            var logToDelete = new TourLogsModel(logId, DateTime.Now, 0, 0, "to be deleted", "", 0, Guid.NewGuid());
            _viewModel.TourLogs.Add(logToDelete);
            _viewModel.SelectedLog = logToDelete;

            _viewModel.DeleteCommand.Execute(null);

            Assert.IsFalse(_viewModel.TourLogs.Contains(logToDelete));
            _mockTourLogsManager.Verify(m => m.DeleteLog(logId), Times.Once);
        }

        [Test]
        public void CanGenerateReport_WhenTourIsSelected_ShouldBeTrue()
        {
            var tour = new TourModel()
                .WithId(Guid.NewGuid())
                .WithName("Sample Tour")
                .WithDate("01.01.2025")
                .WithDuration(0)
                .WithDistance(0)
                .WithImagePath("")
                .WithDescription("")
                .WithFrom("")
                .WithTo("")
                .WithTransportType("")
                .WithTourLogs(new ObservableCollection<TourLogsModel>());

            _tourListViewModel.SelectedTour = tour;
            _tourListViewModel.Tours.Clear();

            bool canExecute = _viewModel.SaveReport.CanExecute(null);

            Assert.IsTrue(canExecute);
        }

        [Test]
        public void CanGenerateReport_WhenNoTourSelectedButToursExistInList_ShouldBeTrue()
        {
            _tourListViewModel.SelectedTour = null;
            TourModel t = new TourModel().WithDate("10.03.2025").WithName("TourModle");
            _tourListViewModel.Tours.Add(t);
            bool canExecute = _viewModel.SaveReport.CanExecute(null);

            Assert.IsTrue(canExecute);
        }

        [Test]
        public void CanGenerateReport_WhenNoTourSelectedAndNoToursInList_ShouldBeFalse()
        {
            _tourListViewModel.SelectedTour = null;
            _tourListViewModel.Tours.Clear();

            bool canExecute = _viewModel.SaveReport.CanExecute(null);

            Assert.IsFalse(canExecute);
        }
    }
}