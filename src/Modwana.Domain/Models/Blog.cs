using Modwana.Core.Entities;
using Modwana.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modwana.Domain.Models
{
    public class Blog : BaseEntity
    {
        [StringLength(2048)]
        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? PublishDate { get; set; }

        public Blog()
        {
            CreatedDate = SystemDate.Now;
        }
    }
}
