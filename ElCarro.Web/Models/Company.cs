using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ElCarro.Web.Models
{
    [Table("Companies")]
    public class Company
    {
        public Company()
        {
            Stores = new HashSet<Store>();
        }

        public int Id { get; set; }
        [Required]
        [MaxLength(75)]
        public string Name { get; internal set; }
        public DbGeography Location { get; set; }
        public ApplicationUser Admin { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}
