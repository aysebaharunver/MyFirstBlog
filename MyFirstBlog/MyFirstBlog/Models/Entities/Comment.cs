using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyFirstBlog.Models.Entities
{
    public class Comment:BaseEntity
    {
        public string Content { get; set; }
        public string Email { get; set; }





        public Article Article { get; set; }

        [ForeignKey("Article")]
        public int ArticleId { get; set; }







        public bool IsActive { get; set; }

    }
}