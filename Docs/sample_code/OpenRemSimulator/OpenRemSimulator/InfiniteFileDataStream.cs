using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using OpenRemSimulator.Common;

namespace OpenRemSimulator
{
    public class InfiniteFileDataStream : IDataStream
    {
        private readonly string fileName;
        private MicroTimer microTimer;
        private const int OffsetStep = 8;
        private int offset;
        private byte[] fileContent;

        public IObservable<byte> DataStream { get; private set; }

        public InfiniteFileDataStream(string fileName)
        {
            this.fileName = fileName;
            this.offset = -OffsetStep;
        }

        public void Open()
        {
            this.fileContent = File.ReadAllBytes(this.fileName);

            //22 microseconds is aprox 44.1k Hz
            this.microTimer = new MicroTimer(22);
            DataStream = Observable.FromEventPattern<MicroTimer.MicroTimerElapsedEventHandler, MicroTimerEventArgs>(
                    handler => microTimer.MicroTimerElapsed += handler,
                    handler => microTimer.MicroTimerElapsed -= handler)
                .SelectMany((val) =>
                {
                    this.offset += OffsetStep;
                    if (offset > fileContent.Length)
                    {
                        this.offset = 0;
                    }

                    return new List<byte>
                    {
                        GetFileByte(offset),
                        GetFileByte(offset + 1),
                        GetFileByte(offset + 2),
                        GetFileByte(offset + 3),
                        GetFileByte(offset + 4),
                        GetFileByte(offset + 5),
                        GetFileByte(offset + 6),
                        GetFileByte(offset + 7),
                    };
                });


        }

        private byte GetFileByte(int byteNumber)
        {
            if (byteNumber >= fileContent.Length)
            {
                return 0;
            }
            return fileContent[byteNumber];
        }

        public void Close()
        {
            this.microTimer.Stop();
        }

        public void Start()
        {
            microTimer.Start();
        }

        public void Stop()
        {
            microTimer.Stop();
            
        }
    }
}
