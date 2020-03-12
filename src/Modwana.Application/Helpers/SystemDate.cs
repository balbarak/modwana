using Modwana.Core;
using Modwana.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Application.Helpers
{
    public class SystemDate : IDateTime
    {
        public DateTime Now => DateTime.Now;

    }
}
