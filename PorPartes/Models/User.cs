using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PorPartes.Models
{
    [Table("User")]
    public class User
    {
        public User()
        {
            Advertisings = new HashSet<Advertising>();
            Reviews = new HashSet<Review>();
            Shops = new HashSet<Shop>();
        }

        public int ID { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Middle_name { get; set; }
        [Required]
        [MaxLength(255)]
        public string First_surname { get; set; }
        [MaxLength(25)]
        public string Second_surname { get; set; }
        [Required]
        [MaxLength(75)]
        public string Password { get; set; }
        public bool Approved { get; set; }

        public int UserTypeId { get; set; }
        [ForeignKey("UserTypeId")]
        public virtual UserType UserType { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Shop> Shops { get; set; }
        public virtual ICollection<Advertising> Advertisings { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
