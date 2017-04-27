namespace LearnOnline.Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetailExamResult")]
    public partial class DetailExamResult
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExamResultID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubjectsID { get; set; }

        public double? Point { get; set; }

        public int? WorkTime { get; set; }

        public virtual ExamResult ExamResult { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
