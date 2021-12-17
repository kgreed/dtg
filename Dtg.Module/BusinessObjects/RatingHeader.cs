using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;
namespace Dtg.Module.BusinessObjects
{
    [NavigationItem("Main")]
    [Table("RatingHeaders")]
    public class RatingHeader
    {
        [Browsable(false)]
        [Key] public int Id { get; set; }
        [Browsable(false)]
        public int GuruId { get; set; }
        [ForeignKey("GuruId")] [Required] public virtual Guru Guru { get; set; }
      
        public DateTime ScoredAt { get; set; }
        [Browsable(false)]
        [Required] public int RaterId { get; set; }
        [ForeignKey("RaterId")] public virtual Rater Rater { get; set; }
        public virtual ICollection<RatingEntry> Entries { get; set; }
    }
}