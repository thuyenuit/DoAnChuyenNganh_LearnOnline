namespace LearnOnline.Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bank()
        {
            ConfirmTransfers = new HashSet<ConfirmTransfer>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string BankName { get; set; }

        public bool? Stauts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConfirmTransfer> ConfirmTransfers { get; set; }
    }
}
