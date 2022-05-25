using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Extender.Abstractions
{
    public interface IFileLoader
    {
        ValueTask<ValueTuple<string, string>> Load(string path, char separator);
        ValueTask<ValueTuple<string,string>> LoadDecrypted(string path,char separator);
    }
}
