namespace LearnOnline.Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            ExamResults = new HashSet<ExamResult>();
            Thematics = new HashSet<Thematic>();
        }

        public int ID { get; set; }

        [StringLength(256)]
        public string Avatar { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        public DateTime? NgaySinh { get; set; }

        public bool? Sex { get; set; }

        [StringLength(15)]
        public string SSN { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        public int ProvincesID { get; set; }

        public int DistrictID { get; set; }

        [Required]
        [StringLength(100)]
        public string UnitName { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public bool? Status { get; set; }

        public int UserGroupID { get; set; }

        public virtual District District { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamResult> ExamResults { get; set; }

        public virtual Province Province { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Thematic> Thematics { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
