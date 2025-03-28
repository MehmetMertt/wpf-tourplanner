using Moq;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using tour_planner.Commands;
using tour_planner.Model;
using tour_planner.ViewModel;
using System.Windows.Input;

namespace TourPlannerUnitTests
{
    [TestFixture]
    public class TourLogsViewModelTests
    {
        private TourLogsViewModel _viewModel;
        private Mock<TourListViewModel> _mockTourListViewModel;
        private TourModel _tourModel;

        [SetUp]
        public void Setup()
        {
            _mockTourListViewModel = new Mock<TourListViewModel>();
            _tourModel = new TourModel { Name = "Test Tour", TourLogs = new ObservableCollection<TourLogsModel>() };

            _mockTourListViewModel.Setup(m => m.SelectedTour).Returns(_tourModel);
            _viewModel = new TourLogsViewModel(_mockTourListViewModel.Object);
        }

        [Test]
        public void Constructor_ShouldInitializeTourLogs()
        {
            Assert.IsNotNull(_viewModel.TourLogs);
            Assert.AreEqual(0, _viewModel.TourLogs.Count);
        }

        [Test]
        public void SelectedTour_Setter_ShouldUpdateTourLogs()
        {
            var newTour = new TourModel
            {
                Name = "New Tour",
                TourLogs = new ObservableCollection<TourLogsModel>
            {
                new TourLogsModel(DateTime.Now, 10, 20)
            }
            };

            _viewModel.SelectedTour = newTour;

            Assert.AreEqual(1, _viewModel.TourLogs.Count);
        }

        [Test]
        public void CanOpenNewPage_ShouldReturnFalse_WhenNoTourSelected()
        {
            _mockTourListViewModel.Setup(m => m.SelectedTour).Returns((TourModel)null);
            _viewModel = new TourLogsViewModel(_mockTourListViewModel.Object);

            bool result = _viewModel.CanOpenNewPage(null);

            Assert.IsFalse(result);
        }

        [Test]
        public void CanOpenNewPage_ShouldReturnTrue_WhenTourSelected()
        {
            _mockTourListViewModel.Setup(m => m.SelectedTour).Returns(new TourModel());
            _viewModel = new TourLogsViewModel(_mockTourListViewModel.Object);

            bool result = _viewModel.CanOpenNewPage(null);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanOpenEditPage_ShouldReturnFalse_WhenNoLogSelected()
        {
            _viewModel.SelectedLog = null;

            bool result = _viewModel.CanOpenEditPage(null);

            Assert.IsFalse(result);
        }

        [Test]
        public void CanOpenEditPage_ShouldReturnTrue_WhenLogSelected()
        {
            _viewModel.SelectedLog = new TourLogsModel(DateTime.Now, 10, 20);

            bool result = _viewModel.CanOpenEditPage(null);

            Assert.IsTrue(result);
        }
    }
}