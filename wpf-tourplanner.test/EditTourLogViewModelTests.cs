using Moq;
using NUnit.Framework;
using System;
using tour_planner.Model;
using tour_planner.ViewModel;
using TourPlanner.Domain;
using TourPlanner.Model;

namespace TourPlanner.Tests
{
    [TestFixture]
    public class EditTourLogViewModelTests
    {
        private Mock<ITourLogsManager> _mockTourLogsManager;
        private TourLogsModel _originalLog;
        private EditTourLogViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _mockTourLogsManager = new Mock<ITourLogsManager>();

            _originalLog = new TourLogsModel(
                Guid.NewGuid(),
                new DateTime(2025, 6, 30),
                120, 
                15.5f,
                "Original Comment",
                "Medium",
                4, 
                Guid.NewGuid()
            );

            _viewModel = new EditTourLogViewModel(_originalLog, _mockTourLogsManager.Object);
        }

        [Test]
        public void Constructor_ShouldCreateEditableCopyOfTourLog()
        {
            Assert.IsNotNull(_viewModel.EditableTourLog);
            Assert.AreNotSame(_originalLog, _viewModel.EditableTourLog, "EditableTourLog should be a new instance.");

            Assert.That(_viewModel.EditableTourLog.Id, Is.EqualTo(_originalLog.Id));
            Assert.That(_viewModel.EditableTourLog.Comment, Is.EqualTo(_originalLog.Comment));
            Assert.That(_viewModel.EditableTourLog.Difficulty, Is.EqualTo(_originalLog.Difficulty));
            Assert.That(_viewModel.EditableTourLog.Rating, Is.EqualTo(_originalLog.Rating));
            Assert.That(_viewModel.EditableTourLog.TourId, Is.EqualTo(_originalLog.TourId));

            Assert.That(_viewModel.EditableTourLog.DateString, Is.EqualTo("30.06.2025"));
            Assert.That(_viewModel.EditableTourLog.DurationString, Is.EqualTo("120"));
            Assert.That(_viewModel.EditableTourLog.DistanceString, Is.EqualTo("15,5"));
        }


        [Test]
        public void EditableTourLog_WhenInvalidDifficultyStringIsSet_HasErrorsShouldBeTrue()
        {
            var editableLog = _viewModel.EditableTourLog;

            editableLog.Difficulty = "not a number";

            Assert.IsTrue(editableLog.HasErrors, "HasErrors should be true for invalid duration.");
            Assert.IsNotNull(editableLog.GetErrors(nameof(editableLog.Difficulty)), "An error should be registered for Difficulty.");
        }

        [Test]
        public void EditableTourLog_WhenValidDateStringIsSet_HasErrorsShouldBeEmpty()
        {
            var editableLog = _viewModel.EditableTourLog;

            editableLog.DateString = "10.03.2025";

            Assert.IsEmpty(editableLog.GetErrors(nameof(editableLog.Date)), "An error should be registered for DateString.");
        }

        [Test]
        public void EditableTourLog_WhenInvalidDateStringIsSet_HasErrorsShouldBeTrue()
        {
            var editableLog = _viewModel.EditableTourLog;

            editableLog.DateString = "not a date";

            Assert.IsTrue(editableLog.HasErrors, "HasErrors should be true for an invalid date.");
            Assert.IsNotNull(editableLog.GetErrors(nameof(editableLog.DateString)), "An error should be registered for DateString.");
        }

        [Test]
        public void EditableTourLog_WhenInvalidDurationStringIsSet_HasErrorsShouldBeTrue()
        {
            var editableLog = _viewModel.EditableTourLog;

            editableLog.DurationString = "not a number";

            Assert.IsTrue(editableLog.HasErrors, "HasErrors should be true for invalid duration.");
            Assert.IsNotNull(editableLog.GetErrors(nameof(editableLog.DurationString)), "An error should be registered for DurationString.");
        }

     
    }
}
