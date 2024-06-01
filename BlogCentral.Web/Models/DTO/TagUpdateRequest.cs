using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BlogCentral.Web.Models.DTO
{
    public class TagUpdateRequest
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }

    }
}
