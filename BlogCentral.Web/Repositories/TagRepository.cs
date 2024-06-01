using BlogCentral.Web.Models.Data;
using BlogCentral.Web.Models.Domain;
using BlogCentral.Web.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BlogCentral.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogCentralDBContext _dbContext;

        public TagRepository(BlogCentralDBContext blogCentralDBContext)
        {
            this._dbContext = blogCentralDBContext;
        }

        public async Task AddTagAsync(TagRequest tagRequest)
        {

            Tag newTag = new Tag() { Name = tagRequest.Name, DisplayName = tagRequest.DisplayName };
            await this._dbContext.Tags.AddAsync(newTag);
            Console.WriteLine($"Tag is no savechanges yet. Tag id is: {newTag.Id}");

            await this._dbContext.SaveChangesAsync();
            Console.WriteLine($"Tag is savechanges now. Tag id is: {newTag.Id}");

            Console.WriteLine($"Tag is created in the db. Tag id is: {newTag.Id}");
        }

        public async Task DeleteTagAsync(Guid? id)
        {
            var tagToDelete = await this._dbContext.Tags.FindAsync(id);

            if(tagToDelete == null )
            {
                return;
            }
            this._dbContext.Tags.Remove(tagToDelete);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TagResponse>> GetAllAsync()
        {
            return await this._dbContext.Tags.Select(tag => new TagResponse()
            {
                Id = tag.Id,
                Name = tag.Name,
                DisplayName = tag.DisplayName
            }).ToListAsync<TagResponse>();
        }

        public async Task<TagResponse?> GetByIdAsync(Guid? id)
        {
            var findTag = await this._dbContext.Tags.FindAsync(id);
            if (findTag == null)
            {
                return null;
            }

            return new TagResponse()
            {
                Id = findTag.Id,
                Name = findTag.Name,
                DisplayName = findTag.DisplayName
            };
        }

        public async Task UpdateTagAsync(TagUpdateRequest tagUpdateRequest)
        {
            Tag tag = new() { Id = tagUpdateRequest.Id, Name = tagUpdateRequest.Name, DisplayName = tagUpdateRequest.DisplayName };

            this._dbContext.Update(tag);
            await this._dbContext.SaveChangesAsync();
        }
    }
}
