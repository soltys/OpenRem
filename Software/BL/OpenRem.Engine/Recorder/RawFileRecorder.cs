using System;
using OpenRem.Common;
using OpenRem.Engine.OS;
using OpenRem.HAL;

namespace OpenRem.Engine
{
    class RawFileRecorder : IRawFileRecorder
    {
        private readonly IAnalyzerCollection analyzerCollection;
        private readonly IFileAccess fileAccess;
        private IDataStream dataStream;
        private IDisposable writingAction;

        public RawFileRecorder(IAnalyzerCollection analyzerCollection, IFileAccess fileAccess)
        {
            this.analyzerCollection = analyzerCollection;
            this.fileAccess = fileAccess;
        }

        public void Start(Guid analyzerGuid, string fileName)
        {
            this.dataStream = this.analyzerCollection[analyzerGuid].Invoke();
            this.dataStream.Open();

            var fileStream = fileAccess.RecreateAlwaysFile(fileName);
            this.writingAction = this.dataStream.RawDataStream
                .StereoSample(PcmEncoding.PCM32Bit)
                .Subscribe(data =>
                    {
                        var buffer = data.RawData;
                        fileStream.Write(buffer, 0, buffer.Length);
                    },
                    () =>
                    {
                        //on complete
                        fileStream.Close();
                    });


            this.dataStream.Start();
        }

        public void Stop()
        {
            this.writingAction.Dispose();
            this.dataStream.Stop();
            this.dataStream.Close();
        }
    }
}
