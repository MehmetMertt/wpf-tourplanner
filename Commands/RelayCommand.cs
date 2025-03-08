using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace tour_planner.Commands
{
    internal class RelayCommand : ICommand
    {
        private readonly Action<object> _Execute;
        //private readonly Func<object, bool> _CanExecute;
        private readonly Predicate<object> _CanExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action<object> ExecuteMethode, Predicate<object> CanExecuteMethode)
        {
            this._Execute = ExecuteMethode;
            this._CanExecute = CanExecuteMethode;
        }

        public bool CanExecute(object parameter)
        {
            return _CanExecute == null || _CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _Execute(parameter);
        }
    }
}
