using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tour_planner.Validation
{
<<<<<<< HEAD:Model/Route.cs
    internal class Route : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public Route(string name)
        {
            _name = name; 
=======
    class InputValidator
    {

        public InputValidator()
        {
>>>>>>> intermediate-handin:wpf-tourplanner/Validation/InputValidator.cs
        }

        public void validate(string value)
        {

<<<<<<< HEAD:Model/Route.cs

=======
        }
>>>>>>> intermediate-handin:wpf-tourplanner/Validation/InputValidator.cs
    }
}
