using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstBlog.Models.Entities
{
    public class Information : BaseEntity
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LinkedIn { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Cv { get; set; }
    }
}