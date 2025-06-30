using System.Collections.ObjectModel;
using TourPlanner.Domain;

namespace tour_planner.Model
{
    public interface ITourManager
    {
        void AddTour(TourModel tour);
        void DeleteTour(Guid id);
        Task<ObservableCollection<TourModel>> getTours();
        void UpdateTour(TourModel tour);
    }
}