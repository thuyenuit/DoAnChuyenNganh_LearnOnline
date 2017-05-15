namespace LearnOnline.Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Document")]
    public partial class Document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Document()
        {
            DetailThematics = new HashSet<DetailThematic>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string DocumentName { get; set; }

        [StringLength(500)]
        public string LinkDocument { get; set; }

        public int UserID { get; set; }

        public int SubjectsID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailThematic> DetailThematics { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual User User { get; set; }
    }
}
