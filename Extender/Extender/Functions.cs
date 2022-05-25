using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Extender
{
    public static class Functions
    {
        public static unsafe IEnumerable<string> Split(char[] separators,string unformat)
        {
            List<string> cases = new List<string>(unformat.Length / 3);
            StringBuilder builder = new StringBuilder(8);
            fixed(char* symbol = unformat)
            {
                for(int x = 0; x < unformat.Length; x++)
                {
                    if(x == (unformat.Length - 1))
                    {
                        builder.Append(symbol[x]);
                        cases.Add(builder.ToString());
                        builder.Clear();
                    }
                    else if (separators.Contains(symbol[x]))
                    {
                        cases.Add(builder.ToString());
                        builder.Clear();
                    }
                    else
                    {
                        builder.Append(symbol[x]);
                    }
                }
            }
            return cases;
        }
    }
}
