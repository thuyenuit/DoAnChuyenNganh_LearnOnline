namespace LearnOnline.Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetailThematic")]
    public partial class DetailThematic
    {
        public int ID { get; set; }

        public int ThematicID { get; set; }

        public int DocumentID { get; set; }

        public int VideoID { get; set; }

        [Required]
        [StringLength(200)]
        public string TitleName { get; set; }

        public bool? Status { get; set; }

        public virtual Document Document { get; set; }

        public virtual Thematic Thematic { get; set; }

        public virtual Video Video { get; set; }
    }
}
