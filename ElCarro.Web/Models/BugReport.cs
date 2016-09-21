namespace ElCarro.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BugReport
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
        [Display(Name = "Description")]
        public string Description { get; set; }
    }

    [NotMapped]
    public class BugReportErrorView
    {
        public string ID { get; set; }
        public string messageError { get; set; }

        public BugReportErrorView() { }

        public BugReportErrorView(string ID, string message)
        {
            this.ID = ID;
            messageError = message;
        }
    }
}