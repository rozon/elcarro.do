using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElCarro.Web.Models
{
    [Table("Reviews")]
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
        public virtual ApplicationUser User { get; set; }
        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }

        public int PuntuationId { get; set; }
        [ForeignKey("PuntuationId")]
        public virtual Puntuation Puntuation { get; set; }
    }
}
