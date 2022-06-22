using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlog.Models
{
    public class Class1
    {
        public int Id { get; set; }

 
        public string Baslik { get; set; }

        public string Aciklama { get; set; }

           
        public DateTime EklenmeTarihi { get; set; }

        public string Resim { get; set; }
        public int KullaniciId { get; set; }
        public int KategoriId { get; set; }
        public string KullaniciAd { get; set; }
    }
}