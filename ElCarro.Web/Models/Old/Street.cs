using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PorPartes.Models
{
    [Table("Street")]
    public class Street
    {
        public Street()
        {

        }

        public int ID { get; set; }
        public int Number { get; set; }
        [Required]
        [MaxLength(75)]
        public string Name { get; set; }
        public int? Floor { get; set; }

        public virtual Address Address { get; set; }
    }
}
