using Modwana.Core.Entities;
using Modwana.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Modwana.Domain.Models
{
    public class Role : IdentityRole , IBaseEntity , ISeedableEntity<Role>
    {
        [StringLength(128)]
        public override string Id { get => base.Id; set => base.Id = value; }

        public LocaleString Description { get; private set; } = new LocaleString();

        public Role Update(Role entity)
        {
            Description.Arabic = entity.Description.Arabic;
            Description.English = entity.Description.English;
            
            return this;
        }
    }
}
