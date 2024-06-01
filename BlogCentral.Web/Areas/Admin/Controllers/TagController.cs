using BlogCentral.Web.Models.Data;
using BlogCentral.Web.Models.Domain;
using BlogCentral.Web.Models.DTO;
using BlogCentral.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogCentral.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        private ITagRepository _tagRepository;
        public TagController(ITagRepository _tagRepository)
        {
            this._tagRepository = _tagRepository;
        }
        public async Task<IActionResult> Index()
        {
            var allTags = await _tagRepository.GetAllAsync();
            return View(allTags);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(TagRequest tagRequest)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }


            await _tagRepository.AddTagAsync(tagRequest);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid? tagId)
        {

            if (tagId == null)
            {
                return NotFound();
            }
            var returnedTag = await this._tagRepository.GetByIdAsync(tagId);
            if (returnedTag == null)
            {
                return NotFound();
            }
            TagUpdateRequest tagUpdateRequest = new TagUpdateRequest() { Id = returnedTag.Id, DisplayName = returnedTag.DisplayName, Name = returnedTag.Name };
            return View(tagUpdateRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TagUpdateRequest tagUpdateRequest)
        {
            if (tagUpdateRequest == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            await this._tagRepository.UpdateTagAsync(tagUpdateRequest);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid? tagId)
        {
            if (tagId == null)
            {
                return BadRequest();
            }
            await this._tagRepository.DeleteTagAsync(tagId);
            return RedirectToAction("Index");
        }
    }
}
