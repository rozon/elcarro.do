using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PorPartes.Models
{
    [Table("Item_Type")]
    public class ItemType
    {
        public ItemType()
        {
            Items = new HashSet<Item>();
        }

        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
