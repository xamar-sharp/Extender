using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Extender.Abstractions
{
    public interface IFileSaver
    {
        ValueTask<bool> SaveAsync(byte[] data, string path, SaverTarget target, string key = null,string customExtension=null);
    }
    public enum SaverTarget
    {
        EncryptData,
        Text,
        Data,
        Json,
        Xml,
        CustomExtension,
        BinaryData,
        Zip,
        Decompress,
        Deflate,
        Brotli,
        GZip
    }
}
