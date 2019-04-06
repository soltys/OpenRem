using System;

namespace OpenRem.HAL
{
    public interface IDataStream
    {
        /// <summary>
        /// Opens and configures port for communication
        /// </summary>
        void Open();

        /// <summary>
        /// Closes port for communication
        /// </summary>
        void Close();

        /// <summary>
        /// Action needed to start receiving data from data stream
        /// </summary>
        void Start();

        /// <summary>
        /// Action needed to stop receiving data from data stream
        /// </summary>
        void Stop();

        /// <summary>
        /// Data stream
        /// </summary>
        IObservable<byte> RawDataStream { get; }
    }
}
