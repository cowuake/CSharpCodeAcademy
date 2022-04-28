using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Library.DesktopClient.Commands
{
    public class RelayCommand : ICommand
    {
        private Action _action;
        private bool _canExecute;

        public RelayCommand(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}