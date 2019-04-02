using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows;

namespace OpenRemSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDataStream abs = new OpenRemDataStream("COM8");
        private const string SampleRaw = "sample.raw";
        private IDisposable instance;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.abs.Open();

            if (File.Exists(SampleRaw))
            {
                File.Delete(SampleRaw);
            }

            var streamWriter = new FileStream(SampleRaw, FileMode.Create, FileAccess.Write);

            this.instance = this.abs.DataStream
                .ObserveOn(TaskPoolScheduler.Default)
                // We buffer 8 bytes
                // 4 bytes for each channel (left and right)
                .Buffer(8)


                // Getting only left channel bits (right is muted)
                // .Select(x => new byte[] { x[0], x[1], x[2], x[3], 0, 0, 0, 0 })
                // Getting only right channel bits (left in muted)
                //.Select(x => new byte[] { 0, 0, 0, 0, x[4], x[5], x[6], x[7] })

                // Right channel as mono
                //.Select(x => new byte[] { x[4], x[5], x[6], x[7] })
                // Left channel as mono
                //.Select(x => new[] { x[0], x[1], x[2], x[3] })
                .SelectMany(x => x)
                .Buffer(TimeSpan.FromMilliseconds(100))
                .Subscribe(
                    (d) =>
                    {
                        var data = d.ToArray();
                        streamWriter.Write(data, 0, data.Length);
                    }, () =>
                    {
                        streamWriter.Close();
                    });

            this.abs.Start();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            this.abs.Stop();
            this.abs.Close();
            this.instance.Dispose();
        }
    }
}
