using BlogCentral.Web.Models.Domain;
using BlogCentral.Web.Models.DTO;

namespace BlogCentral.Web.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<TagResponse>> GetAllAsync();
        Task<TagResponse?> GetByIdAsync(Guid? id);
        Task AddTagAsync(TagRequest tagRequest);
        Task UpdateTagAsync(TagUpdateRequest tagUpdateRequest);
        Task DeleteTagAsync(Guid? id);
    }
}
