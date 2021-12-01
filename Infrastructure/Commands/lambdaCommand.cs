using FileEncryptor.Infrastructure.Commands.Base;
using System;

namespace FileEncryptor.Infrastructure.Commands
{
    internal class LambdaCommand : Command
    {

        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        public LambdaCommand(Action Execute, Func<bool> CanExecute = null)
            : this(
                  execute: p => Execute(),
                  executed: CanExecute is null ? (Func<object, bool>)null : p => CanExecute())
        {

        }
        public LambdaCommand(Action<object> execute, Func<object, bool> executed = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = executed;
        }

        protected override void Execute(object parameter) => _execute?.Invoke(parameter);
        protected override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
    }
}
