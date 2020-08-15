using Modwana.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Persistance
{
    public abstract class DbContextFactory
    {
        public static ModwanaDbContext Create()
        {
            return ServiceLocator.Current.GetService<ModwanaDbContext>();
        }
    }
}
