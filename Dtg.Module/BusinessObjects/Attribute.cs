using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;
namespace Dtg.Module.BusinessObjects
{
    [NavigationItem("Main")]
    [Table("Attributes")]
    public class Attribute
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}