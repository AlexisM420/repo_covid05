using BillingManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BillingManagement.UI.ViewModels.Commands
{
    public class DeleteCustomerCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        Action<Customer> _execute;

        public DeleteCustomerCommand(Action<Customer> action)
        {
            _execute = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter as Customer);
        }
    }
}
