using System;
using System.Collections.Generic;
using System.Text;
using Extender.ViewModels;
namespace Extender.Abstractions
{
    public interface IHttpRequestBearer
    {
        HttpRequestViewModel HttpRequest { get; }
    }
}
