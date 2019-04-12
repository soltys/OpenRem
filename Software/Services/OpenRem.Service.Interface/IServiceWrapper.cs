﻿using OpenRem.Engine;

namespace OpenRem.Service.Interface
{
    public interface IServiceWrapper
    {
        void StartServer();
        void StopServerIfInternalInstance();
    }
}
