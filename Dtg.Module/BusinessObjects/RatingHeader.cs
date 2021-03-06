using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
namespace Dtg.Module.BusinessObjects
{
    [NavigationItem("Main")]
    [Table("RatingHeaders")]
    [DefaultProperty("Summary")]
    public class RatingHeader
    {
        public RatingHeader()
        {
            Entries = new List<RatingEntry>();

        }

        [Browsable(false)]
        [Key] public int Id { get; set; }
        [Browsable(false)]
        public int GuruId { get; set; }
        [ForeignKey("GuruId")] [System.ComponentModel.DataAnnotations.Required] public virtual Guru Guru { get; set; }

        public DateTime ScoredAt { get; set; }
        [Browsable(false)]
        [System.ComponentModel.DataAnnotations.Required] public int RaterId { get; set; }
        [ForeignKey("RaterId")] public virtual Rater Rater { get; set; }

        public string Summary => $"{Rater?.Name} {ScoredAt.Date.ToLocalTime()} {Guru?.Name}";
        [Aggregated]
        public virtual List<RatingEntry> Entries { get; set; }
    }
}