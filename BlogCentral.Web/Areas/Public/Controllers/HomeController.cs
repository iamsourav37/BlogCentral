using BlogCentral.Web.Models;
using BlogCentral.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogCentral.Web.Areas.Public.Controllers
{

    [Area("Public")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ITagRepository _tagRepository;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            this._blogPostRepository = blogPostRepository;
            this._tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            var allBlogs = await _blogPostRepository.GetAllBlogPostAsync();
            var allTags = await _tagRepository.GetAllAsync();
            ViewBag.allTags = allTags;
            return View(allBlogs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
