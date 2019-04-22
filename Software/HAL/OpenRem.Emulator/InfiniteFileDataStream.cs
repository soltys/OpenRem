using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using OpenRem.Common;
using OpenRem.HAL;

namespace OpenRem.Emulator
{
    public class InfiniteFileDataStream : IDataStream
    {
        private readonly string fileName;
        private MicroTimer microTimer;
        private const int OffsetStep = 8;
        private int offset;
        private byte[] fileContent;

        public IObservable<byte> RawDataStream { get; private set; }

        public InfiniteFileDataStream(string fileName)
        {
            this.fileName = fileName;
            this.offset = -InfiniteFileDataStream.OffsetStep;
        }

        public void Open()
        {
            this.fileContent = typeof(InfiniteFileDataStream).Assembly.ReadResourceAllBytes(this.fileName);

            //22 microseconds is aprox 44.1k Hz
            this.microTimer = new MicroTimer(22);
            RawDataStream = Observable.FromEventPattern<MicroTimer.MicroTimerElapsedEventHandler, MicroTimerEventArgs>(
                    handler => this.microTimer.MicroTimerElapsed += handler,
                    handler => this.microTimer.MicroTimerElapsed -= handler)
                .SelectMany((val) =>
                {
                    this.offset += InfiniteFileDataStream.OffsetStep;
                    if (this.offset > this.fileContent.Length)
                    {
                        this.offset = 0;
                    }

                    return new List<byte>
                    {
                        GetFileByte(this.offset),
                        GetFileByte(this.offset + 1),
                        GetFileByte(this.offset + 2),
                        GetFileByte(this.offset + 3),
                        GetFileByte(this.offset + 4),
                        GetFileByte(this.offset + 5),
                        GetFileByte(this.offset + 6),
                        GetFileByte(this.offset + 7),
                    };
                });
        }

        private byte GetFileByte(int byteNumber)
        {
            if (byteNumber >= this.fileContent.Length)
            {
                return 0;
            }

            return this.fileContent[byteNumber];
        }

        public void Close()
        {
            this.microTimer.Stop();
        }

        public void Start()
        {
            this.microTimer.Start();
        }

        public void Stop()
        {
            this.microTimer.Stop();
        }
    }
}