using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Core.Entities
{
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        public string CreatedByUserId { get; set; }

        public string ModifiedByUserId { get; set; }

        public AuditableEntity()
        {
            CreatedDate = DateTime.Now;
        }

        public void InsertAudit()
        {
            this.CreatedDate = DateTime.Now;

            //this.CreatedByUserId = Thread.CurrentPrincipal.GetUserId();
        }

        public void UpdateAudit()
        {
            this.ModifiedDate = DateTime.Now;

            //this.ModifiedByUserId = Thread.CurrentPrincipal.GetUserId();
        }
    }
}
