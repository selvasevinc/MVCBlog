namespace MVCBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yorum")]
    public partial class Yorum
    {
        public int Id { get; set; }

        [Required]
        public string Yazi { get; set; }

        public int KullaniciId { get; set; }

        public int PostId { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual Post Post { get; set; }
    }
}
