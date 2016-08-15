using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PorPartes.Models
{
    [Table("Frequent_Question")]
    public class FrequentQuestion
    {
        public FrequentQuestion()
        {

        }

        public int ID { get; set; }
        [Required]
        [MaxLength(255)]
        public string Question { get; set; }
        [Required]
        public int Frecuency { get; set; }
    }
}
