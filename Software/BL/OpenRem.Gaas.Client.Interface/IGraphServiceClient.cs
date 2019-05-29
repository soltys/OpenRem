using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenRem.Gaas.Client.Interface
{
    public interface IGraphServiceClient
    {
        Task DisplayDataAsync(string name, IEnumerable<DataPoint> dataPoints);
    }
}
