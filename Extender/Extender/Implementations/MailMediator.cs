using System;
using System.Collections.Generic;
using System.Text;
using Extender.Abstractions;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using System.Linq;
using System.IO;
using MimeKit;
using System.Linq;
namespace Extender.Implementations
{
    public class MailMediator:IMailMediator
    {
        private readonly UserCredential _credentials;
        private readonly SmtpClient _client = new SmtpClient();
        public MailMediator(IFileSaver saver)
        {
            _credentials = new UserCredential(saver);
        }
        public async ValueTask DisposeAsync()
        {
            if (_client.IsConnected)
            {
                await _client.DisconnectAsync(true);
            }
            _client.Dispose();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
        }
        public async ValueTask<bool> SendMail(string message,IEnumerable<string> to,IEnumerable<string> attachments)
        {
            try
            {
                var (Login, Password) = _credentials;
                MimeMessage mimeMessage = new MimeMessage()
                {
                    From = { new MailboxAddress("FROM Extender APP", Login) }
                };
                foreach (var toAddress in to)
                {
                    mimeMessage.To.Add(new MailboxAddress("TO Client", toAddress));
                }
                Multipart multipart = new Multipart("mixed");
                foreach (var attachment in attachments)
                {
                    var tuple = Mappings.MimeTypes[Path.GetExtension(attachment)];
                    var part = new MimePart(tuple.Item1, tuple.Item2)
                    {
                        Content = new MimeContent(File.OpenRead(attachment)),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = Path.GetFileName(attachment)
                    };
                    multipart.Add(part);
                }
                multipart.Add(new TextPart("html") { Text = message });
                mimeMessage.Body = multipart;
                var tupleMailInfo = Mappings.MailSettings[Functions.Split(new char[] { '@' }, Login).ToList()[1]];
                await _client.ConnectAsync(tupleMailInfo.Item1, tupleMailInfo.Item2, tupleMailInfo.Item3);
                await _client.AuthenticateAsync(Login, Password);
                string res=await _client.SendAsync(mimeMessage);
                await _client.DisconnectAsync(false);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public async void Authenticate(string login,string password)
        {
            await _credentials.Init(login, password).SaveLocal($"/storage/emulated/0/Android/Data/com.companyname.extender/backup{Guid.NewGuid().ToString().Replace('-', '_')}");
        }
    }
}
