using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
namespace Extender.Abstractions
{
    public interface IProcessFormatter
    {
        string GetInfo(Process process);
    }
}
