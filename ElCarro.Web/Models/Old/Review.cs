using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PorPartes.Models
{
    [Table("Review")]
    public class Review
    {
        public Review()
        {

        }

        public int ID { get; set; }
        [Required]
        [MaxLength(255)]
        [Column("Review")]
        public string _Review { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int ShopId { get; set; }
        [ForeignKey("ShopId")]
        public virtual Shop Shop { get; set; }

        public int PuntuationId { get; set; }
        [ForeignKey("PuntuationId")]
        public virtual Puntuation Puntuation { get; set; }
    }
}
