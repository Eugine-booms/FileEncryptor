using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FileEncryptor.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProgressBar.xaml
    /// </summary>
    public partial class ProgressWindow

    {

        [Description("Статусное сообщение")]
        public string StatusValue
        {
            get { return (string )GetValue(StatusValueProperty); }
            set { SetValue(StatusValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StatusValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusValueProperty =
            DependencyProperty.Register("StatusValue", typeof(string ), typeof(ProgressWindow), new PropertyMetadata("Статус"));




        //[Category("")]


        public double ProgressValue
        {
            get { return (double)GetValue(ProgressValueProperty); }
            set { SetValue(ProgressValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressValueProperty =
            DependencyProperty.Register("ProgressValue", typeof(double), typeof(ProgressWindow), new PropertyMetadata(0.0, OnProgressChanged));

        private static void OnProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           // ProgressView

            var progress_value = (double)e.NewValue;
            var progress_view = ((ProgressWindow)d).ProgressView;
            progress_view.Value = progress_value;
            progress_view.IsIndeterminate = double.IsNaN(progress_value);
        }

        private IProgress<double> _progressInformer;
        public IProgress<double> ProgressInformer => _progressInformer ??= new Progress<double>(p=>ProgressValue=p);

        private IProgress<string> _statusInformer;
        public IProgress<string> StatusInformer => _statusInformer ??= new Progress<string>(s => StatusValue = s);


        private IProgress<(double Percent, string Massage)> _progressStatusInformator;
        public IProgress<(double Percent, string Massage)> ProgressStatusInformator => _progressStatusInformator
            ??= new Progress<(double Percent, string Massage)>(
                p =>
                {
                    ProgressValue = p.Percent;
                    StatusValue = p.Massage;
                });

        private CancellationTokenSource _cancellation;
        public CancellationToken Cancel
        {
            get
            {
                if (_cancellation != null) return _cancellation.Token;
                _cancellation = new CancellationTokenSource();
                CancellButton.IsEnabled = true;
                return _cancellation.Token;
            }
        }
        public ProgressWindow()
        {
            InitializeComponent();
        }

        private void CancellButton_Click(object sender, RoutedEventArgs e) => _cancellation.Cancel();
    }
}
