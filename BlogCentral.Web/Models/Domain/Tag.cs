using System.ComponentModel.DataAnnotations;

namespace BlogCentral.Web.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [Display(Name ="Display Name")]
        public string DisplayName { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
