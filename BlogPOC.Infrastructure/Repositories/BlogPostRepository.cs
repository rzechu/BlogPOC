using BlogPOC.Core.Exceptions;

namespace BlogPOC.Infrastructure.Repositories;

public class BlogPostRepository(BlogContext context) : IBlogPostRepository
{
    public async Task<IEnumerable<BlogPost>> GetAllAsync()
    {
        return await context.BlogPosts.ToListAsync();
    }

    public async Task<BlogPost> GetByIdAsync(int id)
    {
        return await context.BlogPosts.FindAsync(id) ?? throw new NotFoundException(nameof(BlogPost), id.ToString());
    }

    public async Task AddAsync(BlogPost blogPost)
    {
        await context.BlogPosts.AddAsync(blogPost);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(BlogPost blogPost)
    {
        context.Entry(blogPost).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var blogPost = await context.BlogPosts.FindAsync(id);
        if (blogPost != null)
        {
            context.BlogPosts.Remove(blogPost);
            await context.SaveChangesAsync();
        }
    }
}