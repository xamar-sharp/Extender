using System;
using System.Collections.Generic;
using System.Text;
using Extender.Abstractions;
using System.Threading.Tasks;
namespace Extender.Implementations
{
    public class UserCredential : IUserCredential, ICloneable
    {
        private string Login { get; set; }
        private string Password { get; set; }
        private readonly IFileSaver _fileSaver;
        public UserCredential(IFileSaver saver)
        {
            _fileSaver = saver;
        }
        public object Clone()
        {
            return new UserCredential(_fileSaver) { Login = this.Login, Password = this.Password };
        }
        public async ValueTask<bool> SaveLocal(string path, bool encrypt)
        {
            var str = string.Concat(Login, ";", Password);
            return await _fileSaver.SaveAsync(Encoding.Default.GetBytes(str), path, encrypt ? SaverTarget.EncryptData : SaverTarget.BinaryData,
                 SecureData.GenerateSecure(str.Length));
        }
        public static async ValueTask<UserCredential> LoadFrom(string path,bool isEncrypted,IFileLoader loader)
        {
            ValueTuple<string, string> tuple=default;
            if (isEncrypted)
            {
                tuple=await loader.LoadDecrypted(path, ';');
            }
            else
            {
                tuple = await loader.Load(path, ';');
            }
            return new UserCredential(new FileSaver()) { Login=tuple.Item1,Password=tuple.Item2};
        }
        public void Deconstruct(out string login,out string password)
        {
            login = Login ?? "none";
            password = Password ?? "none";
        }
        public IUserCredential Init(string login, string password)
        {
            Login = login;
            Password = password;
            return this;
        }

    }
}
