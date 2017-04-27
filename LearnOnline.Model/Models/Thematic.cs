namespace LearnOnline.Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Thematic")]
    public partial class Thematic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Thematic()
        {
            DetailThematics = new HashSet<DetailThematic>();
        }

        public int ID { get; set; }

        public int SubjectsID { get; set; }

        public int UserID { get; set; }

        [Required]
        [StringLength(256)]
        public string ThematicName { get; set; }

        public decimal? Fees { get; set; }

        public int? NumberOfAccessInMonth { get; set; }

        public int? TotalAccess { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailThematic> DetailThematics { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual User User { get; set; }
    }
}
