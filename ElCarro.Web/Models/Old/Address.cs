using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PorPartes.Models
{
    [Table("Address")]
    public class Address
    {
        public Address()
        {

        }

        public int ID { get; set; }
        [Required]
        [MaxLength(25)]
        public string Zone { get; set; }
        [Required]
        [MaxLength(50)]
        public string Province { get; set; }
        [Required]
        public int Postal_Code { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(15)]
        public string Phone { get; set; }
        [MaxLength(15)]
        public string Cell_phone { get; set; }

        public virtual User User { get; set; }
        public virtual Shop Shop { get; set; }
        public virtual Advertising Advertising { get; set; }
        public virtual Street Street { get; set; }
    }
}
