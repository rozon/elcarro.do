using System.Collections.Generic;

namespace ElCarro.Web.Models
{
    public class Make
    {
        public Make()
        {
            Models = new List<Model>();
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Model> Models { get; set; }
    }
}