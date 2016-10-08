using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElCarro.Web.Models
{
    public class Suggestion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        [Column("Name")]
        [Display(Name = "Nombre")]
        public string NameSug { get; set; }

        [Required(ErrorMessage = "El correo es requerido.")]
        [EmailAddress(ErrorMessage = "El correo no es valido.")]
        [Column("Email")]
        [Display(Name = "Correo")]
        public string EmailSug { get; set; }

        [Required(ErrorMessage = "La sugerencia es requerida.")]
        [Display(Name = "Sugerencia")]
        public string SuggestionMsj { get; set; }
    }
}
