using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.ComponentModel;

namespace TourPlanner.Domain
{
    public class TourLogsModel : ValidatableModelBase, ITourLogsModel
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _dateString;
        public string DateString
        {
            get => _dateString;
            set
            {
                if (string.Equals(_dateString, value, StringComparison.Ordinal)) return;
                _dateString = value;
                OnPropertyChanged(nameof(DateString));
                ValidateDate();
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            private set
            {
                if (_date == value) return;
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private void ValidateDate()
        {
            ClearErrors(nameof(DateString));
            string format = "dd.MM.yyyy";
            if (DateTime.TryParseExact(DateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                Date = parsedDate.Date;
            }
            else if (string.IsNullOrWhiteSpace(DateString))
            {
                AddError(nameof(DateString), "Date cannot be empty.");
            }
            else
            {
                AddError(nameof(DateString), "Date must be in DD.MM.YYYY format.");
            }
        }

        private string _durationString;
        public string DurationString
        {
            get => _durationString;
            set
            {
                if (string.Equals(_durationString, value, StringComparison.Ordinal)) return;
                _durationString = value;
                OnPropertyChanged(nameof(DurationString));
                ValidateDuration();
            }
        }

        private float _duration;
        public float Duration
        {
            get => _duration;
            private set
            {
                if (_duration == value) return;
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        private void ValidateDuration()
        {
            ClearErrors(nameof(DurationString));
            if (float.TryParse(DurationString, NumberStyles.Float, CultureInfo.InvariantCulture, out float parsedValue))
            {
                Duration = parsedValue;
                if (Duration < 0)
                {
                    AddError(nameof(DurationString), "Duration cannot be negative.");
                }
            }
            else if (string.IsNullOrWhiteSpace(DurationString))
            {
                AddError(nameof(DurationString), "Duration cannot be empty.");
            }
            else
            {
                AddError(nameof(DurationString), "Duration must be a valid number.");
            }
        }

        private string _distanceString;
        public string DistanceString
        {
            get => _distanceString;
            set
            {
                if (string.Equals(_distanceString, value, StringComparison.Ordinal)) return;
                _distanceString = value;
                OnPropertyChanged(nameof(DistanceString));
                ValidateDistance();
            }
        }

        private float _distance;
        public float Distance
        {
            get => _distance;
            private set
            {
                if (_distance == value) return;
                _distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }

        private void ValidateDistance()
        {
            ClearErrors(nameof(DistanceString));
            if (float.TryParse(DistanceString, NumberStyles.Float, CultureInfo.InvariantCulture, out float parsedValue))
            {
                Distance = parsedValue;
                if (Distance < 0)
                {
                    AddError(nameof(DistanceString), "Distance cannot be negative.");
                }
            }
            else if (string.IsNullOrWhiteSpace(DistanceString))
            {
                AddError(nameof(DistanceString), "Distance cannot be empty.");
            }
            else
            {
                AddError(nameof(DistanceString), "Distance must be a valid number.");
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (string.Equals(_comment, value, StringComparison.Ordinal)) return;
                _comment = value;
                OnPropertyChanged(nameof(Comment));
                ValidateComment();
            }
        }

        private void ValidateComment()
        {
            ClearErrors(nameof(Comment));
            if (string.IsNullOrWhiteSpace(Comment))
            {
                AddError(nameof(Comment), "Comment cannot be empty.");
            }
        }

        private string _difficulty;
        public string Difficulty
        {
            get => _difficulty;
            set
            {
                if (string.Equals(_difficulty, value, StringComparison.Ordinal)) return;
                _difficulty = value;
                OnPropertyChanged(nameof(Difficulty));
                ValidateDifficulty();
            }
        }

        private void ValidateDifficulty()
        {
            ClearErrors(nameof(Difficulty));
            if (string.IsNullOrWhiteSpace(Difficulty))
            {
                AddError(nameof(Difficulty), "Difficulty cannot be empty.");
            }
        }

        private int _rating;
        public int Rating
        {
            get => _rating;
            set
            {
                if (_rating == value) return;
                _rating = value;
                OnPropertyChanged(nameof(Rating));
                ValidateRating();
            }
        }

        private void ValidateRating()
        {
            ClearErrors(nameof(Rating));
            if (Rating < 1 || Rating > 5)
            {
                AddError(nameof(Rating), "Rating must be between 1 and 5.");
            }
        }

        private Guid _tourId;
        public Guid TourId
        {
            get => _tourId;
            set
            {
                if (_tourId == value) return;
                _tourId = value;
                OnPropertyChanged(nameof(TourId));
            }
        }

        public TourLogsModel()
            : this(Guid.NewGuid(), DateTime.Today, 0, 0, string.Empty, string.Empty, 0, Guid.Empty)
        {
        }

        public TourLogsModel(Guid id, DateTime date, float duration, float distance, string comment, string difficulty, int rating, Guid tourId)
        {
            _id = id;
            _tourId = tourId;
            _comment = comment;
            _difficulty = difficulty;
            _rating = rating;

            DateString = date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            DurationString = duration.ToString(CultureInfo.InvariantCulture);
            DistanceString = distance.ToString(CultureInfo.InvariantCulture);

            ValidateAllProperties();
        }

        private void ValidateAllProperties()
        {
            ValidateDate();
            ValidateDuration();
            ValidateDistance();
            ValidateComment();
            ValidateDifficulty();
            ValidateRating();
        }

        // Fluent builder methods

        public TourLogsModel WithId(Guid id)
        {
            Id = id;
            return this;
        }

        public TourLogsModel WithDate(DateTime date)
        {
            DateString = date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            return this;
        }

        public TourLogsModel WithDuration(float duration)
        {
            DurationString = duration.ToString(CultureInfo.InvariantCulture);
            return this;
        }

        public TourLogsModel WithDistance(float distance)
        {
            DistanceString = distance.ToString(CultureInfo.InvariantCulture);
            return this;
        }

        public TourLogsModel WithComment(string comment)
        {
            Comment = comment;
            return this;
        }

        public TourLogsModel WithDifficulty(string difficulty)
        {
            Difficulty = difficulty;
            return this;
        }

        public TourLogsModel WithRating(int rating)
        {
            Rating = rating;
            return this;
        }

        public TourLogsModel WithTourId(Guid tourId)
        {
            TourId = tourId;
            return this;
        }
    }
}
