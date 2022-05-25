using System;
using System.Collections.Generic;
using System.Text;
using Extender.Abstractions;
using System.Linq;
using Xamarin.Essentials;
namespace Extender.Implementations
{
    public class NetworkWorker:INetworkWorker
    {
        public bool HasNetworkAccess()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet && Connectivity.ConnectionProfiles.Count() != 0;
        }
        public string FormatInfo()
        {
            StringBuilder builder = new StringBuilder(128);
            builder.AppendLine($"{Resource.NetworkAccess}: {Connectivity.NetworkAccess}");
            builder.AppendLine($"{Resource.ConnectionProfiles}: {Connectivity.ConnectionProfiles}");
            return builder.ToString();
        }
    }
}
