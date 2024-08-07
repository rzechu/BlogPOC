using BlogPOC.Application.Services;
using BlogPOC.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlogPOC.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogPostsController : ControllerBase
{
    private readonly BlogPostService _blogPostService;
    private readonly UserService _userService;

    public BlogPostsController(BlogPostService blogPostService, UserService userService)
    {
        _blogPostService = blogPostService;
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPosts()
    {
        var blogPosts = await _blogPostService.GetAllBlogPostsAsync();
        return Ok(blogPosts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BlogPost>> GetBlogPost(int id)
    {
        var blogPost = await _blogPostService.GetBlogPostByIdAsync(id);
        if (blogPost == null)
        {
            return NotFound();
        }

        // Get user details from User API
        var user = await _userService.GetUserByIdAsync(blogPost.Id); // Assuming the author ID is the same as the blog post ID
        if (user != null)
        {
            // Do something with the user data if needed
        }

        return Ok(blogPost);
    }

    [HttpPost]
    public async Task<ActionResult<BlogPost>> PostBlogPost(BlogPost blogPost)
    {
        await _blogPostService.AddBlogPostAsync(blogPost);
        return CreatedAtAction(nameof(GetBlogPost), new { id = blogPost.Id }, blogPost);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBlogPost(int id, BlogPost blogPost)
    {
        if (id != blogPost.Id)
        {
            return BadRequest();
        }
        await _blogPostService.UpdateBlogPostAsync(blogPost);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlogPost(int id)
    {
        await _blogPostService.DeleteBlogPostAsync(id);
        return NoContent();
    }
}