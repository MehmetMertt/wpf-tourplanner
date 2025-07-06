using System.Collections.ObjectModel;
using System.Globalization;

namespace TourPlanner.Domain
{
    public class TourModel : ValidatableModelBase
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set
            {
                if (_id == value) return; // Optional optimization
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                ValidateName();
                OnPropertyChanged(nameof(Name));
            }
        }

        private void ValidateName()
        {
            ClearErrors(nameof(Name));
            if (string.IsNullOrWhiteSpace(Name))
            {
                AddError(nameof(Name), "Name cannot be empty.");
            }
        }

        private string _date;
        public string Date
        {
            get => _date;
            set
            {
                if (_date == value) return;
                _date = value;
                ValidateDate();
                OnPropertyChanged(nameof(Date));
            }
        }

        private void ValidateDate()
        {
            ClearErrors(nameof(Date));
            string format = "dd.MM.yyyy";
            DateTime parsedDate;

            if (DateTime.TryParseExact(Date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate) == false)
            {
                AddError(nameof(Date), "Date is not in the DD.MM.YYYY Format");
            }
        }

        private float _totalDuration;
        public float TotalDuration
        {
            get => _totalDuration;
            set
            {
                // Consider adding an epsilon for float comparisons if exact equality is an issue
                if (_totalDuration == value) return;
                _totalDuration = value;
                ValidateDuration();
                OnPropertyChanged(nameof(TotalDuration));
            }
        }

        private void ValidateDuration()
        {
            ClearErrors(nameof(TotalDuration));
            if (TotalDuration <= 0)
            {
                AddError(nameof(TotalDuration), "The duration must be >= 0 and a valid number");
            }
        }

        private float _totalDistance;
        public float TotalDistance
        {
            get => _totalDistance;
            set
            {
                if (_totalDistance == value) return;
                _totalDistance = value;
                ValidateDistance();
                OnPropertyChanged(nameof(TotalDistance));
            }
        }

        private void ValidateDistance()
        {
            ClearErrors(nameof(TotalDistance));
            if (TotalDistance <= 0)
            {
                AddError(nameof(TotalDistance), "The distance must be >= 0 and a valid number");
            }
        }

        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                if (_imagePath == value) return;
                _imagePath = value;
                ValidateImagePath();
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        private void ValidateImagePath()
        {
            ClearErrors(nameof(ImagePath));
            if (string.IsNullOrWhiteSpace(ImagePath))
            {
                //  AddError(nameof(ImagePath), "ImagePath cannot be empty.");
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (_description == value) return;
                _description = value;
                ValidateDescription();
                OnPropertyChanged(nameof(Description));
            }
        }

        private void ValidateDescription()
        {
            ClearErrors(nameof(Description));
            if (string.IsNullOrWhiteSpace(Description))
            {
                AddError(nameof(Description), "Description cannot be empty.");
            }
        }

        private string _from;
        public string From
        {
            get => _from;
            set
            {
                if (_from == value) return;
                _from = value;
                ValidateFrom();
                OnPropertyChanged(nameof(From));
            }
        }

        private void ValidateFrom()
        {
            ClearErrors(nameof(From));
            if (string.IsNullOrWhiteSpace(From))
            {
                AddError(nameof(From), "Origin cannot be empty.");
            }
        }

        private string _to;
        public string To
        {
            get => _to;
            set
            {
                if (_to == value) return;
                _to = value;
                ValidateTo();
                OnPropertyChanged(nameof(To));
            }
        }

        private void ValidateTo()
        {
            ClearErrors(nameof(To));
            if (string.IsNullOrWhiteSpace(To))
            {
                AddError(nameof(To), "Destination cannot be empty.");
            }
        }

        private string _transportType;
        public string TransportType
        {
            get => _transportType;
            set
            {
                if (_transportType == value) return;
                _transportType = value;
                ValidateTransportType();
                OnPropertyChanged(nameof(TransportType));
            }
        }

        private void ValidateTransportType()
        {
            ClearErrors(nameof(TransportType));
            if (string.IsNullOrWhiteSpace(TransportType))
            {
                AddError(nameof(TransportType), "Transport type cannot be empty.");
            }
        }

        public ObservableCollection<TourLogsModel> TourLogs { get; set; } = new ObservableCollection<TourLogsModel>();

        public TourModel WithId(Guid id)
        {
            Id = id;
            return this;
        }

        public TourModel WithName(string name)
        {
            Name = name;
            return this;
        }

        public TourModel WithDate(string date)
        {
            Date = date;
            return this;
        }

        public TourModel WithDuration(float totalDuration)
        {
            TotalDuration = totalDuration;
            return this;
        }

        public TourModel WithDistance(float totalDistance)
        {
            TotalDistance = totalDistance;
            return this;
        }

        public TourModel WithImagePath(string imagePath)
        {
            ImagePath = imagePath;
            return this;
        }

        public TourModel WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public TourModel WithTo(string to)
        {
            To = to;
            return this;
        }

        public TourModel WithFrom(string from)
        {
            From = from;
            return this;
        }

        public TourModel WithTransportType(string transportType)
        {
            TransportType = transportType;
            return this;
        }

        public TourModel WithTourLogs(ObservableCollection<TourLogsModel> logs)
        {
            TourLogs = logs;
            return this;
        }

    }
}

