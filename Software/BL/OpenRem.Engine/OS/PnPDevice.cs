using System.Collections.Generic;
using System.Management;

namespace OpenRem.Engine.OS
{
    /// <summary>
    /// Access System list of plug and play
    /// </summary>
    class PnPDevice : IPnPDevice
    {
        public IEnumerable<string> GetDevices()
        {
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_PnPEntity");

            foreach (var o in searcher.Get())
            {
                var queryObj = o as ManagementObject;
                if (queryObj?["Caption"] != null)
                {
                    yield return queryObj["Caption"].ToString();
                }
            }
        }
    }
}