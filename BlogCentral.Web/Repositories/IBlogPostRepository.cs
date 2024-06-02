using BlogCentral.Web.Models.DTO;

namespace BlogCentral.Web.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPostResponse>> GetAllBlogPostAsync();
        Task<BlogPostResponse> GetBlogPostByIdAsync(Guid? id);
        Task AddBlogPostAsync(BlogPostRequest blogPostRequest);
        Task UpdateBlogPostAsync(BlogPostUpdateRequest blogPostUpdateRequest);
        Task DeleteBlogPostAsync(Guid? id);
    }
}
