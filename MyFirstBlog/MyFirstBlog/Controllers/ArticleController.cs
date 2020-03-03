using MyFirstBlog.Models;
using MyFirstBlog.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstBlog.Controllers
{
    public class ArticleController : Controller
    {
        ApplicationDbContext db;
        public ActionResult Detail(int id = 0)
        {
            if (id > 0)
            {
                using (db = new ApplicationDbContext())
                {
                    var article = db
                        .Articles // Makaleler tablosunu sorgula
                        .Include("Category") // Category yi joinle
                        .Include("Comments")
                        .FirstOrDefault(i => i.Id == id); // kriteri ver


                    article.Comments = article.Comments.Where(i => i.IsActive == true).ToList();


                    if (article != null)
                    {
                        return View(article);
                    }

                    return HttpNotFound("Makale bulunamadı");
                }
            }
            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(string email, string content, int articleid)
        {
            Comment comment = new Comment
            {
                Content = content,
                Email = email,
                ArticleId = articleid,
            };

            using (db = new ApplicationDbContext())
            {
                db.Entry(comment).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
            }

            return RedirectToAction("Detail",new {id=articleid });

        }

    }
}