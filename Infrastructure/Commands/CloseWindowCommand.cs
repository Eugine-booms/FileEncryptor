using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Markup;

namespace FileEncryptor.Infrastructure.Commands
{
    [MarkupExtensionReturnType(typeof(CloseWindowCommand))]
    [ContentProperty("Executable")]
    class CloseWindowCommand : Base.Command
    {
        
        protected override void Execute(object parameter)
        {
            
            if (parameter is Window window)
            {
                window.Close();
                return;
            }
            if ((window = App.ActiveWindow) != null)
            {
                window.Close();
                return;
            }
            if ((window = App.FocusedWindow) != null)
            {
                window.Close();
                return;
            }

            
        }
        protected override bool CanExecute(object parameter) => (parameter as Window?? App.FocusedWindow??App.ActiveWindow)!=null;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
