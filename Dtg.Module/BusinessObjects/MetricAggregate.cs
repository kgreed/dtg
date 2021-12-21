using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
namespace Dtg.Module.BusinessObjects
{
    [NotMapped]
    [DomainComponent]
    public class MetricAggregate : NonPersistentBaseObject
    {

        [Browsable(false)]
        public virtual Guru Guru { get; set; }
        [Key]
        public string Metric { get; set; }
        [ModelDefault("DisplayFormat", "{0:#.##}")]
        [ModelDefault("EditMask", "g")]
        public decimal Average { get; set; }
        //  public decimal StdDev { get; set; }
    }
}