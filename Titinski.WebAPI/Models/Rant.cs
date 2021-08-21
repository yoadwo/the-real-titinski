using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Titinski.WebAPI.Models
{
    [Table("Rants")]
    public class Rant : IEntity
    {
        public string ID { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
    }
}
