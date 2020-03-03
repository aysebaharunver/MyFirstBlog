using MyFirstBlog.Models;
using MyFirstBlog.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstBlog.Controllers
{
    public class HomeController : Controller
    {
        /*
         Ödev

            * Home Index te sayfalama / paging yapısı uygulanacak
            * Home Indexte makale öngösterim alanıında html etiketlerinin gelmemesi sağlanacak.
            * Arama metninin birden fazla kelime içermesi durumunda her bir kelime için ayrı arama yapılması uygulanacak
            * MVCLogin Projesi örnek alıanrak bloga Login entegrasyonu yapılacak
            * Sağ alanda kategori nin altında bir partial uygulanıp içerisinde bir twitter hesabına ait twitlerin gösterimi sağlanacak
            * Bloglarda etiket / hashtag / tag  kullanımı incelenecek. Nedir? nasıl çalışır? nasıl kullanılır?
            * Her makalenin detay sayfasında en altta sosyal medyada paylaş yapısı entegre edilecek
             
             
             */



        ApplicationDbContext db;
        public ActionResult Index(int? categoryId)
        {
            using (db = new ApplicationDbContext())
            {

                if (categoryId != null)
                {
                    var articlesByCategory = db.Articles
                        .Include("Category")
                        .Where(i => i.CategoryId == categoryId)
                        .ToList();
                    return View(articlesByCategory);
                }

                var last5 = db.Articles
                                   .Include("Category")
                                   .OrderByDescending(i => i.CreatedDate)
                                   .Take(5)
                                   .ToList();

                return View(last5);

            }
        }




        public ActionResult _CategoryListing()
        {
            using (db = new ApplicationDbContext())
            {
                var categories = db.Categories.Where(i => i.IsDeleted == false).ToList();

                return PartialView(categories);

            }
        }



        [HttpPost]
        public ActionResult Search(string searchText)
        {
            // eğer arama metni boş ya da null değilse
            if (!string.IsNullOrEmpty(searchText))
            {
                //metni makalelerin başlığında ya da contentinde içerecek şekilde arama yap

                using (db = new ApplicationDbContext())
                {
                    var result = db.Articles
                         .Include("Category")
                         .Where(i => i.Title.Contains(searchText) || i.Content.Contains(searchText))
                         .ToList();

                    return View("Index", result);
                }
            }
            return View("Index");
        }










        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}