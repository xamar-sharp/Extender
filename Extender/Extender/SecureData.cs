using System;
using System.Collections.Generic;
using System.Text;

namespace Extender
{
    public static class SecureData
    {
        internal static string LastSecureKey { get; private set; }
        private static Random _random = new Random();
        public static unsafe string GenerateSecure(int length)
        {
            if (length > Guid.NewGuid().ToString().Length)
            {
                char* sep = stackalloc char[length];
                for (int x = 0; x < length; x++)
                {
                    sep[x] = (char)_random.Next(0, 65536);
                }
                return LastSecureKey = new string(sep);
            }
            return LastSecureKey = Guid.NewGuid().ToString().Remove(length - 1);
        }
    }
}
