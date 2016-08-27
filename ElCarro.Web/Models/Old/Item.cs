using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PorPartes.Models
{
    [Table("Item")]
    public class Item
    {
        public Item()
        {
            StoreItems = new HashSet<StoreItem>();
        }

        public int ID { get; set; }
        [Required]
        [MaxLength(75)]
        public string Name { get; set; }
        [Required]
        public byte[] Image { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        [Required]
        [Column("Last_View")]
        public DateTime LastView { get; set; }
        [Required]
        public int Popularity { get; set; }
        public bool Interior { get; set; }

        public int Item_TypeId { get; set; }
        [ForeignKey("Item_TypeId")]
        public virtual ItemType ItemType { get; set; }

        public virtual ICollection<StoreItem> StoreItems { get; set; }
    }
}
