using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Archive.UI.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object, bool> canExecute;
        private readonly Func<object, Task> execute;

        public RelayCommand(Func<object, Task> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => canExecute == null || canExecute(parameter);

        public async void Execute(object parameter) => await execute(parameter);
    }
}