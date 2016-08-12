using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PorPartes.Models
{
    [Table("Contact")]
    public class Contact
    {
        public Contact()
        {

        }

        public int ID { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Last_Name { get; set; }
        [Required]
        [MaxLength(75)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string Phone { get; set; }
        [MaxLength(75)]
        public string Social_Network { get; set; }
    }
}
