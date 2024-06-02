using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BlogCentral.Web.Models.DTO
{
    public class BlogPostUpdateRequest
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string? Content { get; set; }
        public string? ShortDescription { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public string? UrlHandle { get; set; }
        public string? Author { get; set; }
        public bool? IsVisible { get; set; }
        public IEnumerable<SelectListItem>? AvailableTags { get; set; }
        public IEnumerable<Guid>? SelectedTags { get; set; }
    }
}
