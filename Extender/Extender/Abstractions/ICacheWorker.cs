using System;
using System.Collections.Generic;
using System.Text;

namespace Extender.Abstractions
{
    public interface ICacheWorker
    {
        bool CheckCache(string key);
    }
}
