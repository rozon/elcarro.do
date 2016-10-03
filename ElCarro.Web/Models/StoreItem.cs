using System.ComponentModel.DataAnnotations.Schema;

namespace ElCarro.Web.Models
{
    [Table("Store_Items")]
    public class StoreItem
    {
        public StoreItem()
        {

        }

        public int ID { get; set; }
        public bool Available { get; set; }

        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }

        public int VehiculeId { get; set; }
        [ForeignKey("VehiculeId")]
        public virtual Vehicle Vehicule { get; set; }

        public int PartId { get; set; }
        [ForeignKey("PartId")]
        public virtual VehiclePart Part { get; set; }
    }
}
