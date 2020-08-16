using Modwana.Persistance;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Modwana.Test
{
    public class DatabaseTest : TestBase
    {
        [Fact]
        public void Add()
        {
            var context = new ModwanaDbContext();

            context.Blogs.Add(new Domain.Models.Blog()
            {
                Body = "ffd"
            });

            context.SaveChanges();
        }
    }
}
