using MyFirstBlog.Models;
using MyFirstBlog.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstBlog.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db;

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        #region Kategori işlemleri
        public ActionResult Categories()
        {
            using (db = new ApplicationDbContext())
            {
                var categories = db.Categories.ToList();
                return View(categories);
            }
        }

        public ActionResult AddOrUpdateCategory(int? id)
        {
            if (id != null)
            {
                // TODO: Buraya güncelleme için gelindiğinde çalışacak kodları yaz
                using (db = new ApplicationDbContext())
                {
                    var category = db.Categories.Find(id);
                    if (category != null) // Tek satır if için scope {} gerekmez
                        return View(category);
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddOrUpdateCategory(Category category)
        {
            using (db = new ApplicationDbContext())
            {
                if (category.Id > 0)
                {
                    db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                    TempData["result"] = "<b>" + category.Name + "</b> güncellendi";
                }
                else
                {
                    // burası ekleme kodları
                    // Yöntem 1
                    // db.Categories.Add(category);
                    // Yöntem 2

                    db.Entry(category).State = System.Data.Entity.EntityState.Added;
                    TempData["result"] = "<b>" + category.Name + "</b> eklendi";
                }
                db.SaveChanges();
            }
            return RedirectToAction("Categories");
        }

        public ActionResult CategoryDelete(int id = 0)
        {
            if (id > 0)
            {
                using (db = new ApplicationDbContext())
                {
                    var category = db.Categories.Find(id);
                    if (category != null)
                    {
                        // Hard-Delete
                        // Cascade delete e sebep olabileceği için SAKIN HA DİKKATSİZ KULLANMAYIN!!!
                        //db.Entry(category).State = System.Data.Entity.EntityState.Deleted;


                        // Soft-Delete
                        category.IsDeleted = true;
                        category.DeletedDate = DateTime.Now;

                        db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                        int result = db.SaveChanges();

                        if (result > 0)
                        {
                            TempData["result"] = result + " adet kayıt silindi";
                        }
                    }
                }
            }

            return RedirectToAction("Categories");
        }


        #endregion

        #region Makale İşlemleri

        public ActionResult Articles()
        {
            using (db = new ApplicationDbContext())
            {
                var categorylist = db.Articles.Include("Category").ToList();


                return View(categorylist);
            }
        }

        public ActionResult AddOrUpdateArticle(int? id)
        {
            using (db = new ApplicationDbContext())
            {
                // Ekleme ya da güncelleme durumalrında kategori listesine hep ihitiyacımız olacağından if e girmeden viewBag e attık
                ViewBag.CategoryList = db.Categories
                    .Where(i => i.IsDeleted == false)
                    .ToList();


                if (id != null)
                {
                    // burası güncelleme alanı
                    // ilgili kaydı bul ve view e gönder

                    var article = db.Articles.Find(id);
                    if (article != null)
                    {
                        return View(article);
                    }
                }
                return View();
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddOrUpdateArticle(Article article)
        {
            using (db = new ApplicationDbContext())
            {
                if (article.Id > 0)
                {
                    db.Entry(article).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.Entry(article).State = System.Data.Entity.EntityState.Added;
                }

                db.SaveChanges();
            }
            return RedirectToAction("Articles");
        }

        public ActionResult DeleteArticle(int id = 0, bool unDelete = false)
        {
            if (id > 0)
            {
                using (db = new ApplicationDbContext())
                {
                    var article = db.Articles.Find(id);
                    if (article != null)
                    {
                        // bu bir geri getirme talebi ise
                        if (unDelete == true)
                        {
                            // silinme durumunu false yap
                            article.IsDeleted = false;
                            // sislinme tarihini null yap
                            article.DeletedDate = null;
                        }
                        else
                        {
                            // silindi durmunu true yap
                            article.IsDeleted = true;
                            // silinme tarihini şu an yap
                            article.DeletedDate = DateTime.Now;
                        }


                        db.Entry(article).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Articles");
        }

        #endregion

        #region Yorum İşlemleri

        public ActionResult CommentList()
        {
            using (db=new ApplicationDbContext())
            {
                var passiveComments = db.Comments.Where(i => i.IsActive == false).ToList();

                return View(passiveComments);
            }
        }

        public ActionResult ApproveComment(int id = 0)
        {
            if (id>0)
            {
                using (db=new ApplicationDbContext())
                {
                    var cmt = db.Comments.Find(id);
                    if (cmt!=null)
                    {
                        cmt.IsActive = true;
                        db.Entry(cmt).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("CommentList");
        }

        public ActionResult DeleteComment(int id = 0)
        {
            if (id > 0)
            {
                using (db = new ApplicationDbContext())
                {
                    var cmt = db.Comments.Find(id);
                    if (cmt != null)
                    {                       
                        db.Entry(cmt).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("CommentList");
        }
        #endregion

    }
}