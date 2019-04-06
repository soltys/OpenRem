using System.IO;

namespace OpenRem.Engine.OS
{
    class FileAccess : IFileAccess
    {
        public Stream RecreateAlwaysFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            return new FileStream(fileName, FileMode.CreateNew);
        }
    }
}
