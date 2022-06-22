using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MVCBlog.Models;



namespace MVCBlog.Controllers
{

    public class HomeController : Controller
    {
        BlogContext db = new BlogContext();

        public ActionResult List(int? id, Kullanici model)
        {
            var postlar = db.Posts
                .Select(i => new Class1()
                {
                    Id = i.Id,
                    Baslik = i.Baslik,
                    Aciklama = i.Aciklama,
                    Resim = i.Resim,
                    EklenmeTarihi = i.EklenmeTarihi,
                    KullaniciId = i.KullaniciId,
                    KullaniciAd = i.Kullanici.KullaniciAd,   

                    KategoriId = i.KategoriId

                }).AsQueryable();
            if (id != null)
            {
                postlar = postlar.Where(i => i.KategoriId == id);
            }

            return View(postlar.ToList());
        }
        public ActionResult Index()
        {
            ViewBag.KategoriId = db.Kategoris.ToList();
            var postlar = db.Posts.OrderByDescending(x => x.Id).ToList();
            return View(postlar);
        }

        public ActionResult Details(int? id)
        {
            var post = db.Posts.Where(i => i.Id == id).SingleOrDefault();
            return View(post);
        }




        //public ActionResult Bilim()
        //{

        //    return View();
        //}
        //public ActionResult Teknoloji()
        //{

        //    return View();
        //}
        //public ActionResult WebTasarim()
        //{

        //    return View();
        //}
        //public ActionResult Dunyanin_Yasini_Nasil_Biliyoruz() 
        //{
        //    return View();
        //}

        //public ActionResult Haptik_Teknoloji()
        //{
        //    return View();
        //}

        //public ActionResult Animasyonlu_Saat_Tasarimi()
        //{
        //    return View();
        //}

      
    }
}