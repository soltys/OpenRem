using System;
using System.Windows;

namespace OpenRem.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.GaasControl.Open();
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            this.GaasControl.Close();
        }
    }
}