using System;
using System.Collections.Generic;
using System.Globalization; // Needed for CultureInfo and NumberStyles
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; // Needed for OnPropertyChanged

namespace TourPlanner.Domain
{
    public class TourLogsModel : ValidatableModelBase
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
                // Use string.Equals for robust string comparison
                if (string.Equals(_dateString, value, StringComparison.Ordinal)) return;
                _dateString = value;
                OnPropertyChanged(nameof(DateString));
                ValidateDate(); // This will parse _dateString and set the internal _date
            }
        }

        // Actual DateTime property
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            // IMPORTANT: Make setter private or internal to control how 'Date' is set.
            // It should primarily be set via the 'DateString' property's validation.
            set // Changed from public to private
            {
                if (_date == value) return;
                _date = value;
                // We typically don't need OnPropertyChanged for 'Date' if 'DateString' is the UI binding.
                // However, if other parts of your code bind to 'Date', keep it.
                // For preventing time display, the key is the .Date on parsedDate.
                OnPropertyChanged(nameof(Date)); // Keep if other parts might consume this directly
            }
        }

        private void ValidateDate()
        {
            // IMPORTANT: Clear errors for the DateString property, as that's what's bound to the UI.
            ClearErrors(nameof(DateString)); // Changed from nameof(Date) to nameof(DateString)
            string format = "dd.MM.yyyy";
            if (DateTime.TryParseExact(DateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                // CRUCIAL: Set the internal Date property with the .Date component
                // This ensures the time part is always midnight (00:00:00).
                Date = parsedDate.Date; // <--- ADDED .Date HERE
            }
            else if (string.IsNullOrWhiteSpace(DateString))
            {
                AddError(nameof(DateString), "Date cannot be empty."); // Changed from nameof(Date)
            }
            else
            {
                AddError(nameof(DateString), "Date must be in DD.MM.YYYY format."); // Changed from nameof(Date)
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
            set // Make setter private to control setting via string property
            {
                if (_duration == value) return;
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        private void ValidateDuration()
        {
            ClearErrors(nameof(DurationString)); // Clear errors for the string property
            if (float.TryParse(DurationString, NumberStyles.Float, CultureInfo.InvariantCulture, out float parsedValue))
            {
                Duration = parsedValue;
                if (Duration < 0)
                {
                    AddError(nameof(DurationString), "Duration cannot be negative."); // Error on string property
                }
            }
            else if (string.IsNullOrWhiteSpace(DurationString))
            {
                AddError(nameof(DurationString), "Duration cannot be empty."); // Error on string property
            }
            else
            {
                AddError(nameof(DurationString), "Duration must be a valid number."); // Error on string property
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
            set // Make setter private to control setting via string property
            {
                if (_distance == value) return;
                _distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }

        private void ValidateDistance()
        {
            ClearErrors(nameof(DistanceString)); // Clear errors for the string property
            if (float.TryParse(DistanceString, NumberStyles.Float, CultureInfo.InvariantCulture, out float parsedValue))
            {
                Distance = parsedValue;
                if (Distance < 0)
                {
                    AddError(nameof(DistanceString), "Distance cannot be negative."); // Error on string property
                }
            }
            else if (string.IsNullOrWhiteSpace(DistanceString))
            {
                AddError(nameof(DistanceString), "Distance cannot be empty."); // Error on string property
            }
            else
            {
                AddError(nameof(DistanceString), "Distance must be a valid number."); // Error on string property
            }
        }


        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (string.Equals(_comment, value, StringComparison.Ordinal)) return; // Use string.Equals
                _comment = value;
                OnPropertyChanged(nameof(Comment)); // OnPropertyChanged after value set
                ValidateComment(); // Validate after property changed
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
                if (string.Equals(_difficulty, value, StringComparison.Ordinal)) return; // Use string.Equals
                _difficulty = value;
                OnPropertyChanged(nameof(Difficulty)); // OnPropertyChanged after value set
                ValidateDifficulty(); // Validate after property changed
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
                OnPropertyChanged(nameof(Rating)); // OnPropertyChanged after value set
                ValidateRating(); // Validate after property changed
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


        public TourLogsModel(Guid id, DateTime date, float duration, float distance, string comment, string difficulty, int rating, Guid tourId)
        {
            _id = id; // Directly assign for constructor
            _tourId = tourId; // Directly assign for constructor
            _comment = comment; // Directly assign
            _difficulty = difficulty; // Directly assign
            _rating = rating; // Directly assign

            // Call setters for string-backed properties to trigger validation and internal type conversion
            DateString = date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture); // This will set _date internally via ValidateDate()
            DurationString = duration.ToString(CultureInfo.InvariantCulture); // This will set _duration internally
            DistanceString = distance.ToString(CultureInfo.InvariantCulture); // This will set _distance internally

            // Ensure initial validation runs for all properties if needed after initial setup
            ValidateAllProperties(); // Assuming this method exists in ValidatableModelBase
        }

        public TourLogsModel() : this(Guid.NewGuid(), DateTime.Today, 0, 0, string.Empty, string.Empty, 0, Guid.Empty)
        {
            // DateTime.Today ensures the initial internal Date has time 00:00:00
        }

        // Helper to validate all properties, useful after initial object creation
        private void ValidateAllProperties()
        {
            ValidateDate();
            ValidateDuration();
            ValidateDistance();
            ValidateComment();
            ValidateDifficulty();
            ValidateRating();
            // Do not call ValidateTourId unless it has validation rules
        }
    }
}