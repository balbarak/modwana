using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Core.Interfaces
{
    public interface ICloneableEntity<T>
    {
        T Clone();
    }
}
