using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PorPartes.Models
{
    [Table("User_Type")]
    public class UserType
    {
        public UserType()
        {
            users = new HashSet<User>();
        }

        public int ID { get; set; }
        [Required]
        [MaxLength(35)]
        public string Type { get; set; }

        public virtual ICollection<User> users { get; set; }
    }
}
