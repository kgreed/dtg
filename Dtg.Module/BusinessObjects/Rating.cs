using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;
namespace Dtg.Module.BusinessObjects
{
    [NavigationItem("Main")]
    [Table("Ratings")]

    public class Rating
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [Required]
        public virtual User User { get; set; }

        public decimal Score { get; set; }

        public int AttributeId { get; set; }
        [ForeignKey("AttributeId")]
        [Required]
        public virtual Attribute Attribute { get; set; }

        public DateTime ScoredAt { get; set; }


    }
}