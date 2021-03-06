﻿using System;
using PhoenixSystem.Engine.System;

namespace PhoenixSystem.Engine.Events
{
    public class SystemRemovedEventArgs : EventArgs
    {
        public SystemRemovedEventArgs(ISystem system)
        {
            System = system;
        }

        public ISystem System { get; set; }
    }
}