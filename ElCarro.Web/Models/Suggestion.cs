using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElCarro.Web.Models
{
    public class Suggestion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Suggestion")]
        public string SuggestionMsj { get; set; }
    }

    [NotMapped]
    public class SuggestionErrorView
    {
        public string ID { get; set; }
        public string messageError { get; set; }

        public SuggestionErrorView() { }

        public SuggestionErrorView(string ID, string message)
        {
            this.ID = ID;
            messageError = message;
        }
    }
}
