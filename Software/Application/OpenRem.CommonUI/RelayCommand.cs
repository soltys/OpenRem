using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OpenRem.CommonUI
{
    public class RelayCommand:ICommand
    {
        private Action action;

        public RelayCommand(Action action)
        {
            this.action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.action();
        }
    }
}