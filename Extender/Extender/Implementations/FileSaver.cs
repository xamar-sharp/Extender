using System;
using System.Collections.Generic;
using System.Text;
using Extender.Abstractions;
using System.Threading.Tasks;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.IO;
namespace Extender.Implementations
{
    public class FileSaver:IFileSaver
    {
        public async ValueTask<bool> SaveAsync(byte[] data,string path,SaverTarget target,string key=null,string customExtension = null)
        {
            switch (target)
            {
                case SaverTarget.BinaryData:
                    return await SaveBinary(path, data);
                case SaverTarget.EncryptData:
                    return await SaveEncrypted(path, data, Encoding.Default.GetBytes(key));
            }
            return false;
        }
        private static async ValueTask<bool> SaveBinary(string path,byte[] data)
        {
            try
            {
                await using (FileStream stream = File.Create(Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + ".dat")))
                {
                    await using(BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write(data);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private static async ValueTask<bool> SaveEncrypted(string path,byte[] srcData,byte[] key)
        {
            if (key.Length != srcData.Length)
            {
                return false;
            }
            try
            {
                BitArray source = new BitArray(srcData);
                BitArray encrypter = new BitArray(key);
                source = source.Xor(encrypter);
                byte[] data = new byte[srcData.Length];
                source.CopyTo(data, 0);
                await using (FileStream stream = File.Create(Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + ".bin")))
                {
                    await using (MemoryStream memory = new MemoryStream(data))
                    {
                        await memory.CopyToAsync(stream);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
