using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogCentral.Web.Models.DTO
{
    public class TagRequest
    {

        public Guid? Id { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(2, ErrorMessage ="Enter atleast 2 character")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Tag Display Name")]
        public string DisplayName { get; set; }
    }
}
