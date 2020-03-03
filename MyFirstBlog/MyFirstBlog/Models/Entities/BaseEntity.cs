using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFirstBlog.Models.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.CreatedDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
