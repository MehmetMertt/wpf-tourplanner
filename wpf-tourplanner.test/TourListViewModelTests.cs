using Moq;
using System.Collections.ObjectModel;
using tour_planner.ViewModel;
using TourPlanner.Domain;

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
            // public TourModel(Guid id, string name, string date, string totalDuration, float totalDistance, string imagePath, string description)

            _mockTourListViewModel = new Mock<TourListViewModel>();
            _tourModel = new TourModel(Guid.NewGuid(), "Wien Tour", "01.01.1990", 3.2f, 1.2f, "", "Wien Tour mit dem Auto","Wien","Wiener Neudorf","Auto",new());

        }

        [Test]
        public void Constructor_ShouldInitializeTourLogs()
        {
            Assert.IsNotNull(_viewModel.TourLogs);
            Assert.AreEqual(0, _viewModel.TourLogs.Count);
        }

        [Test]
        public void InvalidDate_ModelShouldHaveErrors()
        {
            _tourModel.Date = "01.01.19990";

            Assert.IsTrue(_tourModel.HasErrors);
        }

/*        [Test]
        public void CanOpenNewPage_ShouldReturnFalse_WhenNoTourSelected()
        {
            _mockTourListViewModel.Setup(m => m.SelectedTour).Returns((TourModel)null);
            _viewModel = new TourLogsViewModel(_mockTourListViewModel.Object);

            bool result = _viewModel.CanOpenNewPage(null);

            Assert.IsFalse(result);
        }*/

/*        [Test]
        public void CanOpenNewPage_ShouldReturnTrue_WhenTourSelected()
        {
            _mockTourListViewModel.Setup(m => m.SelectedTour).Returns(new TourModel());
            _viewModel = new TourLogsViewModel(_mockTourListViewModel.Object);

            bool result = _viewModel.CanOpenNewPage(null);

            Assert.IsTrue(result);
        }*/

   /*     [Test]
        public void CanOpenEditPage_ShouldReturnFalse_WhenNoLogSelected()
        {
            *//*_viewModel.SelectedLog = null;

            bool result = _viewModel.CanOpenEditPage(null);

            Assert.IsFalse(result);*//*
        }*/

        [Test]
        public void CanOpenEditPage_ShouldReturnTrue_WhenLogSelected()
        {
/*            _viewModel.SelectedLog = new TourLogsModel(DateTime.Now, 10, 20);

            bool result = _viewModel.CanOpenEditPage(null);

            Assert.IsTrue(result);*/
        }
    }
}