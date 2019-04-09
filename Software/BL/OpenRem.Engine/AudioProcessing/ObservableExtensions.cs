using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using OpenRem.Common;

namespace OpenRem.Engine
{
    public static class ObservableExtensions
    {
        public static IObservable<AudioSample> GetSample(this IObservable<byte> observable, PcmEncoding encoding, int channelsCount)
        {
            int bytesPerSide = PcmEncodingHelper.ToByteLength(encoding);
            int bufferSize = bytesPerSide * channelsCount;
            return observable.Buffer(bufferSize).Select(x => new AudioSample(x.ToArray(), encoding, channelsCount));
        }

        public static IObservable<AudioSample> StereoSample(this IObservable<byte> observable, PcmEncoding encoding)
        {
            return observable.GetSample(encoding, 2);
        }

        public static IObservable<AudioSample> SideSample(this IObservable<AudioSample> observable, Side side)
        {
            return observable.Select(sample => sample.ToMono(side));
        }
    }
}
