using BlogCentral.Web.Models.Data;
using BlogCentral.Web.Models.Domain;
using BlogCentral.Web.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BlogCentral.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        private readonly BlogCentralDBContext _dbContext;
        public TagController(BlogCentralDBContext blogCentralDBContext)
        {
            this._dbContext = blogCentralDBContext;
        }
        public IActionResult Index()
        {
            var allTags = this._dbContext.Tags.ToList();
            return View(allTags);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(TagRequest tagRequest)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            Tag newTag = new Tag() { Name = tagRequest.Name, DisplayName = tagRequest.DisplayName };
            this._dbContext.Tags.Add(newTag);
            this._dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid? tagId)
        {

            if (tagId == null)
            {
                return NotFound();
            }
            var returnedTag = this._dbContext.Tags.Find(tagId);
            if (returnedTag == null)
            {
                return NotFound();
            }
            TagUpdateRequest tagUpdateRequest = new TagUpdateRequest() { Id = returnedTag.Id, DisplayName = returnedTag.DisplayName, Name = returnedTag.Name };
            return View(tagUpdateRequest);
        }

        [HttpPost]
        public IActionResult Edit(TagUpdateRequest tagUpdateRequest)
        {

            if (tagUpdateRequest == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return NotFound();
            }


            Tag tag = new Tag() { Id = tagUpdateRequest.Id, Name = tagUpdateRequest.Name, DisplayName = tagUpdateRequest.DisplayName };

            this._dbContext.Update(tag);
            this._dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid? tagId)
        {
            if (tagId == null)
            {
                return BadRequest();
            }
            var tagToBeDelete = this._dbContext.Tags.Find(tagId);
            if (tagToBeDelete == null)
            {
                return BadRequest();
            }

            this._dbContext.Tags.Remove(tagToBeDelete);
            this._dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
