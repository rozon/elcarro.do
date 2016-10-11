namespace ElCarro.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BugReport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, ErrorMessage = "El nombre no puede ser mas grande de 50 caracteres.")]
        [Column("Name")]
        [Display(Name = "Nombre")]
        public string NameBR { get; set; }

        [Required(ErrorMessage = "El correo es requerido.")]
        [EmailAddress(ErrorMessage = "El correo no es valido.")]
        [Column("Email")]
        [Display(Name = "Correo")]
        public string EmailBR { get; set; }

        [Required(ErrorMessage = "La descripción es requerida.")]
        [Column("Description")]
        [Display(Name = "Descripción")]
        public string DescriptionBR { get; set; }
    }
}
