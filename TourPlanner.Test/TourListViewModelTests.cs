using Moq;
using System.Collections.ObjectModel;
using tour_planner.Model;
using tour_planner.ViewModel;
using TourPlanner.BL.ImportExport;
using TourPlanner.Domain;

namespace TourPlannerUnitTests
{
    [TestFixture]
    public class TourListViewModelTests
    {
        private TourLogsViewModel _viewModel;
        private Mock<TourListViewModel> _mockTourListViewModel;
        private TourListViewModel _tourListViewModel;
        private TourModel _tourModel;
        private Mock<ITourManager> _mockTourManager;


        [SetUp]
        public void Setup()
        {
            _mockTourManager = new Mock<ITourManager>();
            _tourListViewModel = new TourListViewModel(_mockTourManager.Object, new TourExportService(), new TourImportService(), null);
            _tourModel = new TourModel()
                .WithId(Guid.NewGuid())
                .WithName("Wien Tour")
                .WithDate("01.01.1990")
                .WithDuration(3.2f)
                .WithDistance(1.2f)
                .WithImagePath("")
                .WithDescription("Wien Tour mit dem Auto")
                .WithFrom("Wien")
                .WithTo("Wiener Neudorf")
                .WithTransportType("Auto")
                .WithTourLogs(new ObservableCollection<TourLogsModel>());

        }



        [Test]
        public void InvalidDate_ModelShouldHaveErrors()
        {
            _tourModel.Date = "01.01.19990";

            Assert.IsTrue(_tourModel.HasErrors);
        }



        [Test]
        public async Task LoadLogShouldImport3Logs()
        {
            var mockTourManager = new Mock<ITourManager>();
            Guid tourId = Guid.NewGuid();

            var tourLogs = new ObservableCollection<TourLogsModel>
    {
        new TourLogsModel(Guid.NewGuid(), DateTime.Now, 2, 10, "Auto", "2", 5, tourId),
        new TourLogsModel(Guid.NewGuid(), DateTime.Now, 2, 10, "Comment2", "2", 5, tourId),
        new TourLogsModel(Guid.NewGuid(), DateTime.Now, 2, 10, "Comment3", "2", 5, tourId),
    };

            var tours = new ObservableCollection<TourModel>
{
    new TourModel()
        .WithId(tourId)
        .WithName("Tour1")
        .WithDate("20.05.2025")
        .WithDuration(3)
        .WithDistance(60)
        .WithImagePath("")
        .WithDescription("Desc")
        .WithFrom("From")
        .WithTo("To")
        .WithTransportType("Car")
        .WithTourLogs(tourLogs)
};


            mockTourManager.Setup(m => m.getTours()).ReturnsAsync(tours);

            var tourListViewModel = new TourListViewModel(
                mockTourManager.Object,
                new TourExportService(),
                new TourImportService(),
                null
            );

            await Task.Delay(50);

            Assert.That(tourListViewModel.Tours.Count, Is.EqualTo(1));
            Assert.That(tourListViewModel.Tours.First().TourLogs.Count, Is.EqualTo(3));
        }


        [Test]
        public void InvalidObjectOnTourFilterShouldReturnFalse()
        {
            var invalidObject = new object();

          bool result = _tourListViewModel.TourFilter(invalidObject);

            Assert.IsFalse(result);
        }

        [Test]
        public void RightFilderWithTextAutoShouldReturnTrue()
        {
            _tourListViewModel.SearchText = "Auto";

            bool result = _tourListViewModel.TourFilter(_tourModel);

            Assert.IsTrue(result);
        }

        [Test]
        public void RightFilderWithNonsenseTextShouldReturnFalse()
        {
            _tourListViewModel.SearchText = "xiwdwd";

            bool result = _tourListViewModel.TourFilter(_tourModel);

            Assert.IsFalse(result);
        }
    }



}
