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

        // Constructor for RelayCommand that takes an action to execute and a predicate to check if it can execute.
        public RelayCommand(Action<object> executeMethod, Predicate<object> canExecuteMethod)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        // Event that notifies when the ability to execute the command has changed.
        public event EventHandler? CanExecuteChanged
        {
            add
            {
                // Register the event handler with the CommandManager's RequerySuggested event.
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                // Unregister the event handler from the CommandManager's RequerySuggested event.
                CommandManager.RequerySuggested -= value;
            }
        }

        // Determines if the command can be executed based on the provided predicate.
        public bool CanExecute(object? parameter)
        {
            // Check if the _canExecuteMethod is null or if the predicate returns true for the given parameter.
            return _canExecuteMethod == null || _canExecuteMethod(parameter);
        }

        // Executes the command by invoking the provided action.
        public void Execute(object? parameter)
        {
            // Execute the _executeMethod action with the provided parameter.
            _executeMethod(parameter);
        }
    }
}
