using Modwana.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Core.Helpers
{
    public class SystemDate
    {
        public static DateTime Now => ServiceLocator.Current.GetService<IDateTime>().Now;
    }
}
