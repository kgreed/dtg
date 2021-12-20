using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;
namespace Dtg.Module.BusinessObjects
{
    [NavigationItem("Main")]
    [Table("Rater")]
    public class Rater
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}