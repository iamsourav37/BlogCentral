using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BlogCentral.Web.Models.DTO
{
    public class BlogPostRequest
    {
        [Required]
        public string Heading { get; set; }
        
        [Required]
        [Display(Name ="Page Title")]
        public string PageTitle { get; set; }
        public string? Content { get; set; }

        [Display(Name = "Short Description")]
        public string? ShortDescription { get; set; }

        [Display(Name = "Featured Image Url")]
        public string? FeaturedImageUrl { get; set; }

        [Display(Name = "Url Handle")]
        public string? UrlHandle { get; set; }

        public string? Author { get; set; }
        public bool IsVisible { get; set; } = false;

        public IEnumerable<SelectListItem>? AvailableTags { get; set; }
        public IEnumerable<Guid>? SelectedTags { get; set; }
    }
}
