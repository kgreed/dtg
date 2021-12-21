using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
namespace Dtg.Module.BusinessObjects
{
    [NavigationItem("Main")]
    [Table("Metrics")]
    public class Metric
    {
        public Metric()
        {
            Entries = new List<RatingEntry>();
        }

        [Key] [Browsable(false)] public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        [Aggregated]
        public virtual List<GuruAggregate> Metrics
        {
            get
            {
                var entryAggregates = from e in Entries
                    group e by e.RatingHeader.Guru.Name
                    into g
                    select new { Guru = g.Key, Average = g.Average(x => x.Score) };
                return entryAggregates.Select(e => new GuruAggregate
                    { Guru = e.Guru, Average = e.Average, Metric = this }).ToList();
            }
        }
        [Aggregated] public virtual List<RatingEntry> Entries { get; set; }
    }
}