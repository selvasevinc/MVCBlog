namespace MVCBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            Yorums = new HashSet<Yorum>();
            Etikets = new HashSet<Etiket>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name ="Baþlýk")]
        [StringLength(100)]
        public string Baslik { get; set; }

        [Required]
        [Display(Name = "Açýklama")]
        [StringLength(1000)]
        public string Aciklama { get; set; }

        [Required]
        [Display(Name = "Ýçerik")]
        public string Icerik { get; set; }

        [Column(TypeName = "Smalldatetime")]
        [Display(Name = "Tarih")]
        public DateTime EklenmeTarihi { get; set; }

        [StringLength(200)]
        public string Resim { get; set; }

        public int KullaniciId { get; set; }

        [Display(Name = "Kategori")]
        public int KategoriId { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorum> Yorums { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Etiket> Etikets { get; set; }
    }
}
