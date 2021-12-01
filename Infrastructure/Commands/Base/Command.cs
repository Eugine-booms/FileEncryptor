using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Markup;

namespace FileEncryptor.Infrastructure.Commands.Base
{
    [MarkupExtensionReturnType(typeof(Command))]
    [ContentProperty("Executable")]
    internal abstract class Command : MarkupExtension, ICommand 
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private bool _executadle=true;

        public bool Executable 
        { 
            get => _executadle;
            set
            {
                if (_executadle == value) return;
                _executadle = value;
                CommandManager.InvalidateRequerySuggested();
            }
        }


        bool ICommand.CanExecute(object parameter) => _executadle && CanExecute(parameter);

        void ICommand.Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                Execute(parameter);
            }
            
        }


        protected virtual bool CanExecute(object parameter) => true;
        protected abstract void Execute(object parameter);
    }
}
