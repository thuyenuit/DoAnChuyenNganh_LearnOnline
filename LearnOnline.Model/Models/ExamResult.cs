namespace LearnOnline.Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamResult")]
    public partial class ExamResult
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExamResult()
        {
            DetailExamResults = new HashSet<DetailExamResult>();
        }

        public int ID { get; set; }

        public int UserID { get; set; }

        public int LevelID { get; set; }

        public DateTime? CreateDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailExamResult> DetailExamResults { get; set; }

        public virtual Level Level { get; set; }

        public virtual User User { get; set; }
    }
}
