using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenRem.Gaas.Service.Client.Interface;

namespace Gaas.Service.Client.Interface
{
    public interface IGraphServiceClient
    {
        Task DisplayDataAsync(string name, IEnumerable<DataPoint> dataPoints);
    }
}
