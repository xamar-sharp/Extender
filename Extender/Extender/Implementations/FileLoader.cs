using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Extender.Abstractions;
using System.Collections;
using System.Linq;
using System.IO;
namespace Extender.Implementations
{
    public class FileLoader : IFileLoader
    {
        public async ValueTask<ValueTuple<string, string>> Load(string path, char separator)
        {
            IList<string> set = Functions.Split(new char[] { separator }, Encoding.Default.GetString(await File.ReadAllBytesAsync(path))).ToList();
            return (set[0], set[1]);
        }
        public async ValueTask<ValueTuple<string, string>> LoadDecrypted(string path, char separator)
        {
            BitArray encrypted = new BitArray(await File.ReadAllBytesAsync(path));
            BitArray key = new BitArray(Encoding.Default.GetByteCount(SecureData.LastSecureKey));
            byte[] data = new byte[SecureData.LastSecureKey.Length];
            encrypted.Xor(key).CopyTo(data, 0);
            IList<string> set = Functions.Split(new char[] { separator }, Encoding.Default.GetString(data)).ToList();
            return (set[0], set[1]);
        }
    }
}
