using BlogCentral.Web.Models.DTO;
using BlogCentral.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCentral.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogPostController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this._tagRepository = tagRepository;
            this._blogPostRepository = blogPostRepository;
        }
        public async Task<IActionResult> Index()
        {

            var allBlogPost = await this._blogPostRepository.GetAllBlogPostAsync();
            return View(allBlogPost);
        }

        public async Task<IActionResult> Add()
        {
            var allTags = await _tagRepository.GetAllAsync();

            var blogPostRequest = new BlogPostRequest()
            {
                AvailableTags = allTags.Select(tag => new SelectListItem
                {
                    Value = tag.Id.ToString(),
                    Text = tag.DisplayName
                }).ToList()
            };
            return View(blogPostRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BlogPostRequest blogPostRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }


                var allTags = await _tagRepository.GetAllAsync();
                var blogPostRequestModel = new BlogPostRequest()
                {
                AvailableTags = allTags.Select(tag => new SelectListItem
                    {
                        Value = tag.Id.ToString(),
                        Text = tag.DisplayName
                    }).ToList()
                };
                return View(blogPostRequestModel);
            }
            await this._blogPostRepository.AddBlogPostAsync(blogPostRequest);
            return RedirectToAction("Index");
        }
    }
}
