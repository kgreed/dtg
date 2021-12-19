using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
namespace Dtg.Module.BusinessObjects
{
    [NavigationItem("Main")]
    [Table("RatingEntries")]
    [VisibleInReports]
    public class RatingEntry
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [Browsable(false)]
        public int RatingHeaderId { get; set; }
        [ForeignKey("RatingHeaderId")]
        public virtual RatingHeader RatingHeader { get; set; }
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        public decimal Score { get; set; }
        [Browsable(false)]
        public int MetricId { get; set; }
        [ForeignKey("MetricId")] [System.ComponentModel.DataAnnotations.Required] public virtual Metric Metric { get; set; }

        public string MetricName => Metric?.Name;
        public string RaterName => RatingHeader.Rater.Name;
        public string GuruName => RatingHeader.Guru.Name;
    }
}