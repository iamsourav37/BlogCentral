using BlogCentral.Web.Models.Domain;
using BlogCentral.Web.Models.DTO;
using BlogCentral.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

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

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? blogPostId)
        {
            if (blogPostId == null)
            {
                return NotFound();
            }
            var blogPostResponse = await this._blogPostRepository.GetBlogPostByIdAsync(blogPostId);
            if (blogPostResponse == null)
            {
                return NotFound();
            }

            var allTags = await this._tagRepository.GetAllAsync();

            var blogPostUpdateRequest = new BlogPostUpdateRequest()
            {
                Id = blogPostResponse.Id,
                Author = blogPostResponse.Author,
                Content = blogPostResponse.Content,
                FeaturedImageUrl = blogPostResponse.FeaturedImageUrl,
                Heading = blogPostResponse.Heading,
                IsVisible = blogPostResponse.IsVisible,
                PageTitle = blogPostResponse.PageTitle,
                ShortDescription = blogPostResponse.ShortDescription,
                UrlHandle = blogPostResponse?.UrlHandle,
                SelectedTags = blogPostResponse?.Tags.Select(tag => tag.Id),
                AvailableTags = allTags.Select(tag => new SelectListItem
                {
                    Value = tag.Id.ToString(),
                    Text = tag.DisplayName
                }).ToList()
            };

            return View(blogPostUpdateRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogPostUpdateRequest blogPostUpdateRequest)
        {

            if(!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }


                var allTags = await this._tagRepository.GetAllAsync();
                var blogPostResponse = await this._blogPostRepository.GetBlogPostByIdAsync(blogPostUpdateRequest.Id);


                var existingblogPost = new BlogPostUpdateRequest()
                {
                    Id = blogPostResponse.Id,
                    Author = blogPostResponse.Author,
                    Content = blogPostResponse.Content,
                    FeaturedImageUrl = blogPostResponse.FeaturedImageUrl,
                    Heading = blogPostResponse.Heading,
                    IsVisible = blogPostResponse.IsVisible,
                    PageTitle = blogPostResponse.PageTitle,
                    ShortDescription = blogPostResponse.ShortDescription,
                    UrlHandle = blogPostResponse?.UrlHandle,
                    SelectedTags = blogPostResponse?.Tags.Select(tag => tag.Id),
                    AvailableTags = allTags.Select(tag => new SelectListItem
                    {
                        Value = tag.Id.ToString(),
                        Text = tag.DisplayName
                    }).ToList()
                };

                return View(existingblogPost);
            }

            await this._blogPostRepository.UpdateBlogPostAsync(blogPostUpdateRequest);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(Guid? blogPostId)
        {
            if(blogPostId == null)
            {
                return NotFound();
            }
            await this._blogPostRepository.DeleteBlogPostAsync(blogPostId);

            return RedirectToAction("Index");
        }
    }
}
