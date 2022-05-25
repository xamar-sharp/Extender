using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
namespace Extender.Abstractions
{
    public abstract class ProcessManipulator
    {
        protected ProcessManipulator()
        {

        }
        public virtual void Kill(Process process)
        {
            new Thread(() => process.Kill(), 8) {IsBackground=false,Priority=ThreadPriority.AboveNormal,CurrentCulture=
                Thread.CurrentThread.CurrentCulture,CurrentUICulture=Thread.CurrentThread.CurrentUICulture,Name=$".processKill{Guid.NewGuid()}"}.Start();
        }
        public virtual unsafe void Start(string fileName,ProcessWindowStyle style,ICollection<string> arguments,string password=null,string username = null)
        {
            new Thread(() =>
            {
                ProcessStartInfo info = new ProcessStartInfo(fileName) { WindowStyle = style };
                foreach(var arg in arguments)
                {
                    info.ArgumentList.Add(arg);
                }
                if(password !=null)
                {
                    fixed(char* symbptr = password)
                    {
                        info.Password = new System.Security.SecureString(symbptr, password.Length);
                        if (username != null)
                        {
                            info.UserName = username;
                        }
                        else
                        {
                            info.UserName = Environment.UserName;
                        }
                    }
                }
                Process.Start(info);
            }, 8)
            {
                IsBackground = false,
                Priority = ThreadPriority.Highest,
                CurrentCulture =
                Thread.CurrentThread.CurrentCulture,
                CurrentUICulture = Thread.CurrentThread.CurrentUICulture,
                Name = $".processStart{Guid.NewGuid()}"
            }.Start();
        }
    }
}
