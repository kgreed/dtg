using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;
namespace Dtg.Module.BusinessObjects
{
    [NavigationItem("Main")]
    [Table("RatingEntries")]
    public class RatingEntry
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }
        [Required]
        [Browsable(false)]
        public int RatingHeaderId { get; set; }
        [ForeignKey("RatingHeaderId")]
        public virtual RatingHeader RatingHeader { get; set; }
        public decimal Score { get; set; }
        [Browsable(false)]
        public int MetricId { get; set; }
        [ForeignKey("MetricId")] [Required] public virtual Metric Metric { get; set; }
    }
}