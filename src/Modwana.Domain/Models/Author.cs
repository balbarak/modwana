using Modwana.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Modwana.Domain.Models
{
    public class Author : BaseEntity
    {
        [ForeignKey(nameof(Id))]
        public User User { get; set; }

        public string Name { get; set; }

        public Author()
        {

        }

        public Author Update(Author entity)
        {
            if (entity == null)
                return this;

            Name = entity.Name;
            
            return this;
        }
    }
}
