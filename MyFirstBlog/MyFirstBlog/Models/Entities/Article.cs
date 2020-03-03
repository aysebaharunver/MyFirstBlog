using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyFirstBlog.Models.Entities
{
    public class Article : BaseEntity
    {
        [Display(Name = "Başlık")]
        public string Title { get; set; }





        [Display(Name = "İçerik")]
        [DataType(DataType.Html)]
        public string Content { get; set; }










        [Display(Name = "Okunma Sayısı")]
        public int Hit { get; set; }

        public Category Category { get; set; }

        [ForeignKey("Category")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }


        public List<Comment> Comments { get; set; }


    }
}