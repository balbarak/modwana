using Modwana.Core.Entities;
using Modwana.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public string AuthorId { get; set; }

        public Author Author { get; set; }

        public ICollection<Comment> Comments { get; private set; } = new HashSet<Comment>();

        [NotMapped]
        public int NumberOfComments { get; set; }

        public Blog()
        {
            CreatedDate = SystemDate.Now;
        }

        public Blog Update(Blog entity)
        {
            Title = entity.Title;
            Body = entity.Body;

            return this;
        }

    }
}
