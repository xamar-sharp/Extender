using System;
using System.Collections.Generic;
using System.Text;

namespace Extender.Abstractions
{
    public interface INetworkWorker
    {
        bool HasNetworkAccess();
        string FormatInfo();
    }
}
