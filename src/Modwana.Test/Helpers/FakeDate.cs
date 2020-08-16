using Modwana.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Test.Helpers
{
    public class FakeDate : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
