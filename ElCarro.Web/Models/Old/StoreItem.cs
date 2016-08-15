using System.ComponentModel.DataAnnotations.Schema;

namespace PorPartes.Models
{
    [Table("Store_Item")]
    public class StoreItem
    {
        public StoreItem()
        {

        }

        public int ID { get; set; }
        public bool Available { get; set; }

        public int ShopId { get; set; }
        [ForeignKey("ShopId")]
        public virtual Shop Shop { get; set; }

        public int ModelId { get; set; }
        [ForeignKey("ModelId")]
        public virtual ModelVehicle ModelVehicle { get; set; }

        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }
    }
}
