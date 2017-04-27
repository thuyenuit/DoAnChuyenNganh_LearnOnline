namespace LearnOnline.Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConfirmTransfer")]
    public partial class ConfirmTransfer
    {
        public int ID { get; set; }

        public DateTime? DateOfTransfer { get; set; }

        public decimal AmountTransfer { get; set; }

        public int BankTransferPerson { get; set; }

        [StringLength(50)]
        public string BankAccount { get; set; }

        [StringLength(500)]
        public string Messages { get; set; }

        [StringLength(100)]
        public string UsernameEnjoy { get; set; }

        public virtual Bank Bank { get; set; }
    }
}
