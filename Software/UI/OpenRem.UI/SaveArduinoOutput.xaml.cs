using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace OpenRem.UI
{
    /// <summary>
    /// Interaction logic for SaveArduinoOutput.xaml
    /// </summary>
    public partial class SaveArduinoOutput : UserControl
    {
        private const int BaudRate = 9600;
        private readonly SerialPort port = new SerialPort("COM8",
            SaveArduinoOutput.BaudRate, Parity.None, 8, StopBits.One);

        private const int readBufferSize = 512;
        private readonly byte[] readBuffer = new byte[SaveArduinoOutput.readBufferSize];
        private FileStream soundFileHandle;
        private Thread serialPortThread;

        public SaveArduinoOutput()
        {
            InitializeComponent();
        }

        private void BtnStartRecording_Click(object sender, RoutedEventArgs e)
        {
            this.serialPortThread = new Thread(SerialPortProgram);
            this.serialPortThread.Start();
        }

        private void BtnStopRecording_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => { this.status.Text = "Stopped recording"; });
            this.port.DataReceived -= port_DataReceived;
            this.port.Close();
            this.soundFileHandle.Flush(true);
            this.soundFileHandle.Close();
        }

        private void SerialPortProgram()
        {
            if (File.Exists("sound.raw"))
            {
                File.Delete("sound.raw");
            }

            this.soundFileHandle = File.Open("sound.raw", FileMode.CreateNew);

            this.port.DataReceived += port_DataReceived;
            while (!this.port.IsOpen)
            {
                try
                {
                    this.port.Open();
                }
                catch (IOException)
                {
                    Dispatcher.Invoke(() => { this.status.Text = "Cannot connect to COM8, trying again"; });
                    Thread.Sleep(500);
                }
            }

            Dispatcher.Invoke(() => { this.status.Text = "Started recording"; });
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int bytesToRead = this.port.BytesToRead;

            while (bytesToRead > 0)
            {
                try
                {
                    var readBytes = this.port.Read(this.readBuffer, 0, SaveArduinoOutput.readBufferSize);
                    this.soundFileHandle.Write(this.readBuffer, 0, readBytes);
                    bytesToRead -= readBytes;
                }
                catch 
                {
                    //ignoring, probably file was closed before this event happened.
                }
            }
        }
    }
}
