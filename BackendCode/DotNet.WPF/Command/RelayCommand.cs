using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DotNet.WPF.Command
{
    public class RelayCommand : ICommand
    {
        private Action<object> _executeMethod;
        private Predicate<object> _canExecuteMethod;


        public RelayCommand(Action<object> executeMethod, Predicate<object> canExecuteMethod)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }


        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }

        }

        public bool CanExecute(object? parameter)
        {
            return this._canExecuteMethod == null || this._canExecuteMethod(parameter);
        }

        public void Execute(object? parameter)
        {
            this._executeMethod(parameter);
        }
    }
}
