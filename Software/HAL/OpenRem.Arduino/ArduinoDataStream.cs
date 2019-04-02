using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reactive.Linq;
using OpenRem.Common;

namespace OpenRem.Arduino
{
    public class ArduinoDataStream : IDataStream
    {
        private SerialPort serialPort;
        
        private readonly string comPort;
        private readonly ArduinoType arduinoType;

        public IObservable<byte> DataStream { get; private set; }

        public ArduinoDataStream(string comPort, ArduinoType arduinoType)
        {
            this.comPort = comPort;
            this.arduinoType = arduinoType;
        }

        public void Open()
        {
            this.serialPort = ArduinoSerialPortFactory.Create(this.comPort, this.arduinoType);
            this.serialPort.Open();
            DataStream = Observable.FromEventPattern<
                SerialDataReceivedEventHandler,
                SerialDataReceivedEventArgs>
            (
                handler => this.serialPort.DataReceived += handler,
                handler => this.serialPort.DataReceived -= handler
            ).SelectMany(_ =>
            {
                var buffer = new byte[1024];
                var ret = new List<byte>();
                var toRead = this.serialPort.BytesToRead;
                while (toRead > 0)
                {
                    int bytesRead = this.serialPort.Read(buffer, 0, buffer.Length);
                    ret.AddRange(buffer.Take(bytesRead));
                    toRead -= bytesRead;
                }

                return ret;
            });
        }

        public void Close()
        {
            this.serialPort.Close();
        }

        public void Start()
        {
            var message = new byte[] { 1 };
            this.serialPort.Write(message, 0, 1);
        }

        public void Stop()
        {
            var message = new byte[] { 0 };
            this.serialPort.Write(message, 0, 1);
        }
    }
}
