using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PorPartes.Models
{
    [Table("Advertising")]
    public class Advertising
    {
        public Advertising()
        {
            
        }

        public int ID { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        public byte[] Logo { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual Address Address { get; set; }
    }
}
