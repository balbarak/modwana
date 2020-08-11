using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Modwana.Core
{
    public static class Language
    {
        public static bool IsEnglish
        {
            get
            {
                return Thread.CurrentThread.CurrentCulture.Name.Contains("en");

            }
        }
    }
}
