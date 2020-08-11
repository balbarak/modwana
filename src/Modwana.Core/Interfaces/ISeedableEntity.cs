using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Core.Interfaces
{
    public interface ISeedableEntity<T>
    {
        T Update(T entity);
    }
}
