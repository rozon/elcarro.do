using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElCarro.Web.Models
{
    [Table("StoreAddress")]
    public class StoreAddress
    {
        public StoreAddress() { }

        public StoreAddress(StoreAddressView storeAddress)
        {
            Zone = storeAddress.Zone;
            Province = storeAddress.Province;
            City = storeAddress.City;
            StreetName = storeAddress.StreetName;
            StreetNumber = storeAddress.StreetNumber;
        }

        [Key, ForeignKey("Store")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StoreID { get; set; }
        [Required]
        [MaxLength(25)]
        public string Zone { get; set; }
        [Required]
        [MaxLength(50)]
        public string Province { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(75)]
        public string StreetName { get; set; }
        [Required]
        public int StreetNumber { get; set; }
        public virtual Store Store { get; set; }
    }

    [NotMapped]
    public class StoreAddressView
    {
        public StoreAddressView() { }

        public StoreAddressView(StoreAddress storeAddress)
        {
            StoreID = storeAddress.StoreID;
            Zone = storeAddress.Zone;
            Province = storeAddress.Province;
            City = storeAddress.City;
            StreetName = storeAddress.StreetName;
            StreetNumber = storeAddress.StreetNumber;
        }

        public int StoreID { get; set; }
        [Required]
        [MaxLength(25)]
        public string Zone { get; set; }
        [Required]
        [MaxLength(50)]
        public string Province { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(75)]
        public string StreetName { get; set; }
        [Required]
        public int StreetNumber { get; set; }
    }
}
