using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCBlog.Models;

namespace MVCBlog.Controllers
{
    public class PostController : Controller
    {
        private BlogContext db = new BlogContext();

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
                    KullaniciAd=i.Kullanici.KullaniciAd,

                    KategoriId = i.KategoriId

                }).AsQueryable();
            if (id != null)
            {
                postlar = postlar.Where(i => i.KategoriId == id);
            }

            return View(postlar.ToList());
        }

        // GET: Post
        public ActionResult Index(int? id)
        {
            ViewBag.KategoriId = db.Kategoris.ToList();
            var postlar = db.Posts.OrderByDescending(x => x.Id).ToList();   
            return View(postlar);
        }

        public ActionResult Yazilarim()
        {
            var kullaniciadi = Session["username"].ToString();
            var postlar = db.Kullanicis.Where(a => a.KullaniciAd == kullaniciadi).SingleOrDefault().Posts.ToList();
            return View(postlar);
        }

        // GET: Post/Details/5
        public ActionResult Details(int? id)
        {
            var post = db.Posts.Where(i => i.Id == id).SingleOrDefault();
            return View(post);

        }

        // GET: Post/Create
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoris, "Id", "KategoriAd");
            return View();
        }

        // POST: Post/Create
        [HttpPost]   
        public ActionResult Create(Post model)
        {
            try
            {
                if (Request.Files.Count>0)
                {
                    string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string yol = "~/Resimler/" + dosyaadi + uzanti;
                    Request.Files[0].SaveAs(Server.MapPath(yol));
                    model.Resim = "/Resimler/" + dosyaadi + uzanti;
                    
                }
                string kullaniciAdi = Session["username"].ToString();
                var kullanici = db.Kullanicis.Where(i => i.KullaniciAd == kullaniciAdi).SingleOrDefault();
                model.KullaniciId = kullanici.Id;
                model.EklenmeTarihi = DateTime.Now;
                db.Posts.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Kullanici");

            }
            catch 
            {      
                return View();
            }                   
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int id)
        {
            string kullaniciadi = Session["username"].ToString();
            var post = db.Posts.Where(i => i.Id == id).SingleOrDefault();
            if (post==null)
            {
               return HttpNotFound();
            }
            if (post.Kullanici.KullaniciAd==kullaniciadi)
            {
                ViewBag.KategoriId = new SelectList(db.Kategoris, "Id", "KategoriAd");
                return View(post);
            }
            return HttpNotFound();
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]  
        public ActionResult Edit(int id, Post model, HttpPostedFileBase resim)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string yol = "~/Resimler/" + dosyaadi + uzanti;
                    Request.Files[0].SaveAs(Server.MapPath(yol));
                    model.Resim = "/Resimler/" + dosyaadi + uzanti;

                }
                var post = db.Posts.Where(i => i.Id == id).SingleOrDefault();
                post.Baslik = model.Baslik;  
                post.Icerik = model.Icerik;
                post.Resim = model.Resim;
                post.KategoriId = model.KategoriId;
                db.SaveChanges();
                return RedirectToAction("Yazilarim");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int? id)
        {
            var kullaniciadi = Session["username"].ToString();
            var kullanici = db.Kullanicis.Where(i => i.KullaniciAd == kullaniciadi).SingleOrDefault();
            var post = db.Posts.Where(i => i.Id == id).SingleOrDefault();
            foreach(var i in post.Yorums.ToList())
            {
                db.Yorums.Remove(i);
                db.SaveChanges();
            }

            db.Posts.Remove(db.Posts.Find(id));
            db.SaveChanges();
            return RedirectToAction("Yazilarim");
        }
                       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult YorumYap(string yorum, int Postid)
        {

            var kullaniciadi = Session["username"].ToString();
            var kullanici = db.Kullanicis.Where(i => i.KullaniciAd == kullaniciadi).SingleOrDefault();



            db.Yorums.Add(new Yorum
            {
                KullaniciId = kullanici.Id,
                PostId = Postid,
                Yazi = yorum
            });
            db.SaveChanges();


            return Json(false, JsonRequestBehavior.AllowGet);

        }

        //[HttpGet]
        //public PartialViewResult YorumYap(int id)
        //{
        //    ViewBag.deger = id;
        //    return PartialView();
        //}

        //[HttpPost]
        //public PartialViewResult YorumYap(Yorum y)
        //{
        //    db.Yorums.Add(y);
        //    db.SaveChanges();
        //    return PartialView();
        //}
    }
}
