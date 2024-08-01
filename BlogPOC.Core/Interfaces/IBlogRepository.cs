namespace BlogPOC.Core.Interfaces;

public interface IBlogPostRepository : IGenericRepository<BlogPost>
{
    Task<IEnumerable<BlogPost>> GetAllAsync();
    Task<BlogPost> GetByIdAsync(int id);
    Task AddAsync(BlogPost blogPost);
    Task UpdateAsync(BlogPost blogPost);
    Task DeleteAsync(int id);
}