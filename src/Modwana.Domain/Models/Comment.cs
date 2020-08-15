using Modwana.Core;
using Modwana.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using System.Text;

namespace Modwana.Domain.Models
{
    public class Comment : BaseEntity
    {
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Text { get; set; }

        public string BlogId { get; set; }

        [ForeignKey(nameof(BlogId))]
        public Blog Blog { get; set; }

        public string IPAddress { get; set; }

        public string UserAgent { get; set; }

        public Comment()
        {
            Date = DateTime.Now;
        }

        public bool IsAllowedToDelete()
        {
            var principal = ServiceLocator.Current.GetService<IPrincipal>();

            if (!principal.Identity.IsAuthenticated)
                return false;

            return principal.IsInRole(AppRoles.ADMIN_ROLE);
        }
    }
}
