using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
namespace Dtg.Module.BusinessObjects
{
    [NavigationItem("Main")]
    [Table("Gurus")]
    public class Guru
    {
        public Guru()
        {
            Ratings = new List<RatingHeader>();
        }

        [System.ComponentModel.DataAnnotations.Key] [Browsable(false)] public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped] public int NumRatings => Ratings.Count;
        [NotMapped]
        [DevExpress.ExpressApp.DC.Aggregated]
        public virtual List<MetricAggregate> Metrics
        {
            get
            {
                var entries = new List<RatingEntry>();
                foreach (var rating in Ratings) entries.AddRange(rating.Entries);
                var entryAggregates = from e in entries
                    group e by e.Metric.Name
                    into g
                    select new { Metric = g.Key, Average = g.Average(x => x.Score) };
                return entryAggregates.Select(e => new MetricAggregate
                    { Metric = e.Metric, Average = e.Average, Guru = this }).ToList();
            }
        }
        [DevExpress.ExpressApp.DC.Aggregated] public virtual List<RatingHeader> Ratings { get; set; }

        //[ModelDefault("RowCount", "5")]
        //[ToolTip("Select link and right click to navigate to it.")]
        //[Size(SizeAttribute.Unlimited)]
        //[EditorAlias(EditorAliases.RichTextPropertyEditor)]
      //  [EditorAlias(EditorAliases.HtmlPropertyEditor)]
        public string Info { get; set; }
        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
        public byte[]? Memo { get; set; }
    }
}