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
        [Browsable(false)]
        public virtual RatingHeader RatingHeader { get; set; }
        [ModelDefault("DisplayFormat", "{0:g}")]
        [ModelDefault("EditMask", "g")]
        //[DataType("decimal(18,5)")] //https://stackoverflow.com/questions/8243008/format-of-the-initialization-string-does-not-conform-to-specification-starting-a
        public decimal Score { get; set; }
        [Browsable(false)]
        public int MetricId { get; set; }
        [ForeignKey("MetricId")] [System.ComponentModel.DataAnnotations.Required] public virtual Metric Metric { get; set; }
        [Browsable(false)]
        public string MetricName => Metric?.Name;
        public string RaterName => RatingHeader.Rater.Name;
        public string GuruName => RatingHeader.Guru.Name;
        //[Browsable(false)] public int GuruId => RatingHeader.GuruId;
         
    }
}