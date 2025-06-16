using System.Collections.ObjectModel;
using TourPlanner.DAL.Queries;
using TourPlanner.DAL.Queries.Tour;
using TourPlanner.Domain;

namespace TourPlanner.Model
{
    public class TourLogsManager
    {
        private readonly CreateTourLogQuery _createTourLogQuery;
        private readonly DeleteTourLogQuery _deleteTourLogQuery;
        private readonly UpdateTourLogQuery _updateTourLogQuery;
        private readonly GetTourLogsByTourIdQuery _getTourLogsByTourIdQuery;

        public TourLogsManager(
            CreateTourLogQuery createTourLogQuery,
            DeleteTourLogQuery deleteTourLogQuery,
            UpdateTourLogQuery updateTourLogQuery,
            GetTourLogsByTourIdQuery getTourLogsByTourIdQuery)
        {
            _createTourLogQuery = createTourLogQuery;
            _deleteTourLogQuery = deleteTourLogQuery;
            _updateTourLogQuery = updateTourLogQuery;
            _getTourLogsByTourIdQuery = getTourLogsByTourIdQuery;
        }

        public async Task<ObservableCollection<TourLogsModel>> GetLogsForTour(Guid tourId)
        {
            var logs = await _getTourLogsByTourIdQuery.Execute(tourId);
            return new ObservableCollection<TourLogsModel>(logs);
        }



        public async void AddLogToTour(Guid tourId, TourLogsModel log)
        {
            log.TourId = tourId;
            await _createTourLogQuery.Execute(log,tourId);
        }

        public async void UpdateLog(TourLogsModel log)
        {
            await _updateTourLogQuery.Execute(log);
        }

        public async void DeleteLog(Guid logId)
        {
            await _deleteTourLogQuery.Execute(logId);
        }
    }
}
