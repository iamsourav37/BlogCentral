using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BlogCentral.Web.Models.DTO
{
    public class BlogPostResponse
    {

        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string? Content { get; set; }
        public string? ShortDescription { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? UrlHandle { get; set; }
        public string? Author { get; set; }
        public bool? IsVisible { get; set; } = false;
        public IEnumerable<TagResponse> Tags { get; set; }
    }
}
