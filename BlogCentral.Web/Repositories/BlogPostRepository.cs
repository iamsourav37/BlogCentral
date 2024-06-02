using BlogCentral.Web.Models.Data;
using BlogCentral.Web.Models.Domain;
using BlogCentral.Web.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BlogCentral.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogCentralDBContext _blogCentralDBContext;
        private readonly ITagRepository _tagRepository;

        public BlogPostRepository(BlogCentralDBContext blogCentralDBContext, ITagRepository tagRepository)
        {
            this._blogCentralDBContext = blogCentralDBContext;
            this._tagRepository = tagRepository;
        }


        public async Task AddBlogPostAsync(BlogPostRequest blogPostRequest)
        {
            // get the tags from db
            var selectedTags = new List<Tag>();
            foreach (var tagId in blogPostRequest.SelectedTags)
            {
                TagResponse tagFromDb = await this._tagRepository.GetByIdAsync(tagId);
                if (tagFromDb != null)
                {
                    var existingTag = await this._blogCentralDBContext.Tags.FindAsync(tagFromDb.Id);
                    if (existingTag != null)
                    {
                        selectedTags.Add(existingTag);
                    }
                }
            }
            BlogPost newBlogPost = new BlogPost()
            {
                Heading = blogPostRequest.Heading,
                Author = blogPostRequest.Author,
                Content = blogPostRequest.Content,
                FeaturedImageUrl = blogPostRequest.FeaturedImageUrl,
                IsVisible = blogPostRequest.IsVisible,
                ShortDescription = blogPostRequest.ShortDescription,
                PageTitle = blogPostRequest.PageTitle,
                Tags = selectedTags
            };
            this._blogCentralDBContext.BlogPosts.Add(newBlogPost);
            await this._blogCentralDBContext.SaveChangesAsync();
        }

        public async Task DeleteBlogPostAsync(Guid? id)
        {
            var existingBlogPost = await this._blogCentralDBContext.BlogPosts.FindAsync(id);
            if (existingBlogPost == null)
            {
                throw new ArgumentException("BlogPost id is null");
            }
            this._blogCentralDBContext.BlogPosts.Remove(existingBlogPost);
            await this._blogCentralDBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogPostResponse>> GetAllBlogPostAsync()
        {
            var allBlogPost = await _blogCentralDBContext.BlogPosts.Include(x => x.Tags).Select(blogPost => new BlogPostResponse()
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                Heading = blogPost.Heading,
                IsVisible = blogPost.IsVisible,
                PageTitle = blogPost.PageTitle,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                Tags = blogPost.Tags.Select(tag => new TagResponse() { Id = tag.Id, DisplayName = tag.DisplayName, Name = tag.Name }),


            }).ToListAsync();

            return allBlogPost;
        }

        public async Task<BlogPostResponse> GetBlogPostByIdAsync(Guid? id)
        {
            var blogPost = await this._blogCentralDBContext.BlogPosts.Include(blgPost => blgPost.Tags).FirstOrDefaultAsync(blogPost => blogPost.Id == id);
            var blogPostResponse = new BlogPostResponse()
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                Heading = blogPost.Heading,
                IsVisible = blogPost.IsVisible,
                PageTitle = blogPost.PageTitle,
                ShortDescription = blogPost.ShortDescription,
                UrlHandle = blogPost?.UrlHandle,
                Tags = blogPost?.Tags.Select(tag => new TagResponse() { Id = tag.Id, Name = tag.Name, DisplayName = tag.DisplayName }).ToList()
            };

            return blogPostResponse;
        }

        public async Task UpdateBlogPostAsync(BlogPostUpdateRequest blogPostUpdateRequest)
        {
            var selectedTagIds = blogPostUpdateRequest.SelectedTags;
            var selectedTags = await this._blogCentralDBContext.Tags
                .Where(tag => selectedTagIds.Contains(tag.Id))
                .ToListAsync();

            // Fetch the existing blog post from the database
            var existingBlogPost = await this._blogCentralDBContext.BlogPosts
                .Include(bp => bp.Tags)
                .FirstOrDefaultAsync(bp => bp.Id == blogPostUpdateRequest.Id);

            if (existingBlogPost == null)
            {
                throw new Exception("Blog post not found.");
            }


            // Update the blog post properties
            existingBlogPost.Author = blogPostUpdateRequest.Author;
            existingBlogPost.Content = blogPostUpdateRequest.Content;
            existingBlogPost.FeaturedImageUrl = blogPostUpdateRequest.FeaturedImageUrl;
            existingBlogPost.Heading = blogPostUpdateRequest.Heading;
            existingBlogPost.IsVisible = blogPostUpdateRequest.IsVisible;
            existingBlogPost.PageTitle = blogPostUpdateRequest.PageTitle;
            existingBlogPost.ShortDescription = blogPostUpdateRequest.ShortDescription;
            existingBlogPost.UrlHandle = blogPostUpdateRequest.UrlHandle;

            // Clear existing tags and set the new tags
            existingBlogPost.Tags.Clear();
            existingBlogPost.Tags = selectedTags;

            this._blogCentralDBContext.BlogPosts.Update(existingBlogPost);
            await this._blogCentralDBContext.SaveChangesAsync();

        }
    }
}
