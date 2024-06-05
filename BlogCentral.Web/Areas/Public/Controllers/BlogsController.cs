using BlogCentral.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogCentral.Web.Areas.Public.Controllers
{

    [Area("Public")]
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogsController(IBlogPostRepository blogPostRepository)
        {
            this._blogPostRepository = blogPostRepository;
        }

        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await this._blogPostRepository.GetBlogPostByUrlHandle(urlHandle);
            return View(blogPost);
        }
    }
}
