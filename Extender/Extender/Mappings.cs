using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
namespace Extender
{
    public static class Mappings
    {
        public static IDictionary<string, ProcessWindowStyle> WindowStyles;
        public static IDictionary<string, ValueTuple<string, string>> MimeTypes;
        public static IDictionary<string, ValueTuple<string, short, bool>> MailSettings;
        public static IList<string> ImageTypes;
        static Mappings()
        {
            ImageTypes = new List<string>(3) { "image/jpeg", "image/png", "image/gif" };
            WindowStyles = new Dictionary<string, ProcessWindowStyle>(4)
            {
                ["MAX"] = ProcessWindowStyle.Maximized,
                ["MIN"] = ProcessWindowStyle.Minimized,
                ["HIDDEN"] = ProcessWindowStyle.Hidden,
                ["DEFAULT"] = ProcessWindowStyle.Normal
            };
            MimeTypes = new Dictionary<string, (string, string)>(11)
            {
                [".png"] = ("image", "png"),
                [".jpg"] = ("image", "jpeg"),
                [".jpeg"] = ("image", "jpeg"),
                [".json"] = ("application", "json"),
                [".xml"] = ("application", "xml"),
                [".txt"] = ("text", "plain"),
                [".mp4"] = ("video", "mp4"),
                [".mp3"] = ("audio", "mpeg"),
                [".gif"] = ("image", "gif"),
                [".aac"] = ("audio", "aac"),
                [".pdf"] = ("application", "pdf"),
                [".docs"] = ("application", "vnd.openxmlformats-officedocument.wordprocessingml.document")
            };
            MailSettings = new Dictionary<string, (string, short, bool)>(8)
            {
                ["gmail.com"]=("smtp.gmail.com",587,false),
                ["mail.ru"]=("smtp.mail.ru",465,true),
                ["yandex.ru"]=("smtp.yandex.ru",465,true),
                ["masterhost.ru"]=("smtp.masterhost.ru",465,true),
                ["hosting.reg.ru"]=("mail.hosting.reg.ru",465,true),
                ["mail.me.com"]=("smtp.mail.me.com",587,true),
                ["mail.yahoo.com"]=("smtp.mail.yahoo.com",465,true),
                ["inbox.lv"]=("smtp.inbox.lv",587,false)
            };
        }
    }
}
