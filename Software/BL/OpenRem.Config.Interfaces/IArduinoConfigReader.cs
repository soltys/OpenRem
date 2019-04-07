using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenRem.Config
{
    public interface IArduinoConfigReader
    {
        void GetConfig(string name);
    }
}
