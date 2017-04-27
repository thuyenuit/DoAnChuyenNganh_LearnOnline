namespace LearnOnline.Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetailExam")]
    public partial class DetailExam
    {
        public int ID { get; set; }

        public int ExamID { get; set; }

        [Required]
        public string Question { get; set; }

        [StringLength(256)]
        public string OptionA { get; set; }

        [StringLength(256)]
        public string OptionB { get; set; }

        [StringLength(256)]
        public string OptionC { get; set; }

        [StringLength(256)]
        public string OptionD { get; set; }

        [StringLength(256)]
        public string Answer { get; set; }

        public virtual Exam Exam { get; set; }
    }
}
