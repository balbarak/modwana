using Modwana.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Modwana.Domain.Models
{
    public class User : IdentityUser , IBaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [StringLength(128)]
        public override string Id { get => base.Id; set => base.Id = value; }
        public virtual ICollection<IdentityUserRole<string>> Roles { get; } = new HashSet<IdentityUserRole<string>>();

        public DateTime CreatedDate { get; set; }

        public Author Author { get; set; }

        public User()
        {
            this.CreatedDate = DateTime.Now;
        }

        public User Update(User entity)
        {
            Email = entity.Email;
            UserName = entity.UserName;
            NormalizedUserName = entity.UserName.ToUpper();
            NormalizedEmail = entity.Email.ToUpper();

            if (Author == null)
                Author = new Author();

            Author.Update(entity.Author);

            Roles.Clear();

            foreach (var item in entity.Roles)
            {
                Roles.Add(new IdentityUserRole<string>()
                {
                    RoleId = item.RoleId
                });
            }

            return this;

        }
    }
}
