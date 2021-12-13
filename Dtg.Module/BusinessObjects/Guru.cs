using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;
namespace Dtg.Module.BusinessObjects
{
    [NavigationItem("Main")]
    [Table("Gurus")]
    public class Guru
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


    }
}