using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class Func
    {
        public static string Short(string s, int len = 30)
        {
            if (s.Length < len)
                return s;
            return s.Substring(0, 10) + "..." + s.Substring(s.Length - (len - 10), (len - 10));
        }

        public static string fullTime(DateTime T)
        {
            return T.ToLongDateString() + " " + T.ToLongTimeString();
        }
    }
}
