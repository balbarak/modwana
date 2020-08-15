using Modwana.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Persistance
{
    public abstract class UnitOfWorkFactory
    {
        public static IUnitOfWork Create()
        {
            return new UnitOfWork();
        }
    }
}
