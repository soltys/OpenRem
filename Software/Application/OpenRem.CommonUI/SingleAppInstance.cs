using System.Threading;

namespace OpenRem.CommonUI
{
    /// <summary>
    /// Allows to register application for single instance run
    /// </summary>
    public static class SingleAppInstance
    {
        private static Mutex singleAppMutex;

        /// <summary>
        /// Register application mutex and returns true if succeed.
        /// True = this instance of application is single
        /// False = there is already application running with such name!
        /// </summary>
        /// <param name="name">Name of the application</param>
        /// <returns></returns>
        public static bool TryRegister(string name)
        {
            var mutexId = $"Global\\REMedy-{name}";
            var mx = new Mutex(true, mutexId, out var created);
            if (created)
            {
                SingleAppInstance.singleAppMutex = mx;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Release and single app mutex
        /// </summary>
        /// <param name="name"></param>
        public static void Release(string name)
        {
            try
            {
                SingleAppInstance.singleAppMutex?.ReleaseMutex();
            }
            catch
            {
            }
        }
    }
}