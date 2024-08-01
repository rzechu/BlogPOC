namespace BLOGPOC.Application.Services;
public class BlogPostService(IBlogPostRepository repository)
{
    public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<BlogPost> GetBlogPostByIdAsync(int id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task AddBlogPostAsync(BlogPost blogPost)
    {
        await repository.AddAsync(blogPost);
    }

    public async Task UpdateBlogPostAsync(BlogPost blogPost)
    {
        await repository.UpdateAsync(blogPost);
    }

    public async Task DeleteBlogPostAsync(int id)
    {
        await repository.DeleteAsync(id);
    }
}