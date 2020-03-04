using Modwana.Core.Entities;
using Modwana.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Domain.Models
{
    public class Role : IdentityRole , IBaseEntity , ISeedableEntity<Role>
    {
        public LocaleString Description { get; private set; } = new LocaleString();

        public Role Update(Role entity)
        {
            Description.Arabic = entity.Description.Arabic;
            Description.English = entity.Description.English;
            
            return this;
        }
    }
}
