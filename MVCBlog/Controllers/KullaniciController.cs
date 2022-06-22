using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBlog.Models;

namespace MVCBlog.Controllers
{
    public class KullaniciController : Controller
    {
        BlogContext db = new BlogContext();
        // GET: Kullanici
        public ActionResult Index()
        {
            var postlar = db.Posts.ToList();
            return View(postlar);
        }

        // GET: Kullanici/Details/5
      
        public ActionResult Yazilarim()
        {
            var kullaniciadi = Session["username"].ToString();
            var postlar = db.Kullanicis.Where(a => a.KullaniciAd == kullaniciadi).SingleOrDefault().Posts.ToList();
            return View(postlar);
        }

        public ActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GirisYap(Kullanici model)
        {
            try
            {
                var varMi = db.Kullanicis.Where(i => i.KullaniciAd == model.KullaniciAd).SingleOrDefault();
                if (varMi== null)
                {
                    return View();
                }
                if (varMi.Sifre==model.Sifre)
                {
                    Session["username"] = varMi.KullaniciAd;
                   return  RedirectToAction("Index","Kullanici");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {

               
            }
            return View();
        }


        // GET: Kullanici/Create
        public ActionResult KayitOl()
        {
            return View();
        }

        // POST: Kullanici/Create
        [HttpPost]
        public ActionResult KayitOl(Kullanici model)
        {
            try
            {
                var varMi = db.Kullanicis.Where(i => i.KullaniciAd == model.KullaniciAd).SingleOrDefault();
                if (varMi !=null)
                {
                    return View();
                }
                if (string.IsNullOrEmpty(model.Sifre))
                {
                    return View();
                }
                db.Kullanicis.Add(model);
                db.SaveChanges();
                Session["username"] = model.KullaniciAd;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CikisYap()
        {
            Session["username"] = null;
            return RedirectToAction("Index","Home");
        }

        

        // GET: Kullanici/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Kullanici/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
