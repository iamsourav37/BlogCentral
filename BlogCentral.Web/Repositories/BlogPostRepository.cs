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
                    if(existingTag != null)
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

        public Task DeleteBlogPostAsync(Guid? id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPostResponse>> GetAllBlogPostAsync()
        {
            var allBlogPost = await _blogCentralDBContext.BlogPosts
                .Select(blogPost => new BlogPostResponse()
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

            #region Include tags
            //var allBlogPost2 = await _blogCentralDBContext.BlogPosts.Include("Tags").Select(blogPost => new BlogPostResponse()
            //{
            //    Id = blogPost.Id,
            //    Author = blogPost.Author,
            //    Content = blogPost.Content,
            //    FeaturedImageUrl = blogPost.FeaturedImageUrl,
            //    Heading = blogPost.Heading,
            //    IsVisible = blogPost.IsVisible,
            //    PageTitle = blogPost.PageTitle,
            //    PublishedDate = blogPost.PublishedDate,
            //    ShortDescription = blogPost.ShortDescription,
            //    Tags = blogPost.Tags.Select(tag => new TagResponse() { Id = tag.Id, DisplayName = tag.DisplayName, Name = tag.Name }),


            //}).ToListAsync();
            #endregion

            return allBlogPost;
        }

        public Task<BlogPostResponse> GetBlogPostByIdAsync(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBlogPostAsync()
        {
            throw new NotImplementedException();
        }
    }
}
