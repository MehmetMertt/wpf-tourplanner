using System.Collections.ObjectModel;
using TourPlanner.Domain;

namespace TourPlanner.Model
{
    public interface ITourLogsManager
    {
        void AddLogToTour(Guid tourId, TourLogsModel log);
        void DeleteLog(Guid logId);
        Task<ObservableCollection<TourLogsModel>> GetLogsForTour(Guid tourId);
        void UpdateLog(TourLogsModel log);
    }
}