using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Extender.Abstractions
{
    public interface IMailMediator:IAsyncDisposable
    {
        void Authenticate(string email, string password);
        ValueTask<bool> SendMail(string message, IEnumerable<string> to,IEnumerable<string> attachmentsPath);
    }
}
