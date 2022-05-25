using System;
using System.Collections.Generic;
using System.Text;

namespace Extender.Models
{
    public class StartProcessModel
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public IList<string> Arguments { get; set; }
        public string FilePath { get; set; }
        public string WindowStyle { get; set; }
    }
}
