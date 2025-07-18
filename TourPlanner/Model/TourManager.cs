﻿using System.Collections.ObjectModel;
using TourPlanner.DAL.Queries.Tour;
using TourPlanner.Domain;

namespace tour_planner.Model
{
    public class TourManager : ITourManager
    {
        public static ObservableCollection<TourModel> _DatabaseRoute = new ObservableCollection<TourModel>();

        public CreateTourQuery _createTourQuery;
        public DeleteTourQuery _deleteTourQuery;
        public UpdateTourQuery _updateTourQuery;
        public GetAllToursQuery _getAllToursQuery;

        public TourManager(CreateTourQuery createTourQuery,
            DeleteTourQuery deleteTourQuery,
            UpdateTourQuery updateTourQuery,
            GetAllToursQuery getAllToursQuery)
        {
            _createTourQuery = createTourQuery;
            _deleteTourQuery = deleteTourQuery;
            _updateTourQuery = updateTourQuery;
            _getAllToursQuery = getAllToursQuery;
        }

        public async Task<ObservableCollection<TourModel>> getTours()
        {
            IEnumerable<TourModel> tourList = await _getAllToursQuery.Execute();

            ObservableCollection<TourModel> tourCollection = new ObservableCollection<TourModel>(tourList);

            return tourCollection;
        }


        public async void AddTour(TourModel tour)
        {
            await _createTourQuery.Execute(tour);
            _DatabaseRoute.Add(tour);
        }

        public async void UpdateTour(TourModel tour)
        {
            await _updateTourQuery.Execute(tour);
            //_DatabaseRoute.Update(tour);
        }

        public async void DeleteTour(Guid id)
        {
            await _deleteTourQuery.Execute(id);
            /*  TourModel x = _DatabaseRoute.Select(c => c.Id = id);
              _DatabaseRoute.Remove(x);*/
        }
    }
}
