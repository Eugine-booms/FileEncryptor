using System;


namespace FileEncryptor
{
   internal class Programm
    {
        [STAThread]
        
        public static void Main(string [] args)
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
