using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Extender.Abstractions
{
    public interface IUserCredential
    {
        IUserCredential Init(string login, string password);
        ValueTask<bool> SaveLocal(string fileName,bool encrypt = false);
    }
}
