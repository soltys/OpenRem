using System.IO;

namespace OpenRem.Common
{
    public static class StreamHelper
    {
        public static byte[] ReadAllBytes(this Stream input)
        {
            byte[] buffer = new byte[3 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}