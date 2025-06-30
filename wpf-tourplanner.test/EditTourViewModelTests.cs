using Moq;
using tour_planner.Model;
using tour_planner.View;
using tour_planner.ViewModel;
using TourPlanner.Domain;
using TourPlanner.Model;


namespace TourPlanner.Tests
{
    [TestFixture]
    public class AddTourViewModelsSimpleTests
    {
        private Mock<ITourManager> _mockTourManager;
        private Mock<MapView> _mockMapView;
        private TourModel _tour;

        private Mock<ITourLogsManager> _mockLogsManager;
        private TourLogsModel _log;

        [SetUp]
        public void Setup()
        {
            _mockTourManager = new Mock<ITourManager>();
            _mockMapView = new Mock<MapView>();
            var id = Guid.NewGuid();
            _tour = new TourModel().WithId(id).WithName("Test Tour").WithDate(DateTime.Now.ToString("dd.MM.yyyy")).WithFrom("A").WithTo("B").WithTransportType("Auto");

            _mockLogsManager = new Mock<ITourLogsManager>();
            _log = new TourLogsModel().WithId(id).WithComment("Test comment");
        }






        [Test]
        public void AddTourLogViewModel_CanAddTour_ReturnsFalse_WhenEditableTourLogHasErrors()
        {
            var vm = new AddTourLogViewModel(_log, _mockLogsManager.Object);
            vm.EditableTourLog = new TourLogsModel();
            Assert.IsFalse(vm.SaveCommandLog.CanExecute(null));
        }

        [Test]
        public void AddTourLogViewModel_CanAddTour_ReturnsFalse_WhenEditableTourLogIsValid()
        {
            var vm = new AddTourLogViewModel(_log, _mockLogsManager.Object);
            vm.EditableTourLog = _log;
            Assert.IsFalse(vm.SaveCommandLog.CanExecute(null));
        }

        [Test]
        public void AddTourLogViewModel_ToggleIsActionEnabledProperty()
        {
            var vm = new AddTourLogViewModel(_log, _mockLogsManager.Object);
            vm.IsActionEnabled = true;
            Assert.IsTrue(vm.IsActionEnabled);
            vm.IsActionEnabled = false;
            Assert.IsFalse(vm.IsActionEnabled);
        }
    }

}
