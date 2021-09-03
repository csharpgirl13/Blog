using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Contracts;
using Blog.Model;
using Blog.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace Blog.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BlogPostController : ControllerBase
	{
		private readonly ILogger<BlogPostController> _logger;
		private readonly IBlogPostRepository _blogPostRepository;
		private readonly ICommentRepository _commentRepository;
		private readonly IEntityRepository<User> _userRepository;
		public BlogPostController(ILogger<BlogPostController> logger, IBlogPostRepository blogPostRepository, IEntityRepository<BlogPost> entityRepository, ICommentRepository commentRepository, IEntityRepository<User> userRepository)
		{
			_logger = logger;
			_blogPostRepository = blogPostRepository;
			_commentRepository = commentRepository;
			_userRepository = userRepository;
		}

		[HttpGet()]
		[ActionName(nameof(GetAllPostsByUserId))]
		[SwaggerResponse(200, typeof(ICollection<BlogPost>), Description = "OK")]
		[SwaggerResponse(400, typeof(BadRequestResult), Description = "Please, provide valid User Id")]
		[SwaggerResponse(404, typeof(NotFoundResult), Description = "There were not blog posts found by the provided user Id")]
		public IActionResult GetAllPostsByUserId(long userId)
		{
			if (userId == 0)
				return BadRequest("User Id must be greater then 0");
			
			ICollection<BlogPost> blogPosts = _blogPostRepository.GetAllPostsByUserId(userId);
			
			if (!blogPosts.Any())
				return NotFound();

			return Ok(blogPosts);
		}

		[HttpGet("{postId}", Name = nameof(GetPostById))]
		[ActionName(nameof(GetPostById))]
		[SwaggerResponse(200, typeof(BlogPost), Description = "OK")]
		[SwaggerResponse(400, typeof(BadRequestResult), Description = "Please, provide valid Post Id")]
		[SwaggerResponse(404, typeof(NotFoundResult), Description = "There was no blog post found by the provided blog post Id")]
		public IActionResult GetPostById(long postId)
		{
			if (postId == 0)
				return BadRequest("Post Id must be greater then 0");

			var blogPost = _blogPostRepository.GetById(postId);

			if (blogPost == null)
				return NotFound();

			return Ok(blogPost);
		}


		[HttpPost()]
		[SwaggerResponse(201, typeof(BlogPost), Description = "A new blog post has been successfully created.")]
		[SwaggerResponse(404, typeof(NotFoundResult), Description = "User not found")]
		public IActionResult PostNewBlogPost([FromBody] BlogPostAddRequest blogPostUpdateRequest)
		{
			var user = _userRepository.GetById(blogPostUpdateRequest.UserId);
			if (user == null)
				return NotFound($"User with Id {blogPostUpdateRequest.UserId} could not be found");

			BlogPost blogPost = new BlogPost
			{
				Body = blogPostUpdateRequest.Body, 
				Title = blogPostUpdateRequest.Title, 
				Summary = blogPostUpdateRequest.Summary,
				User = user,
				CreatedAt = DateTime.UtcNow
			};

			var newBlogPost =_blogPostRepository.Insert(blogPost);

			return CreatedAtRoute(nameof(GetPostById), new
			{
				postId = newBlogPost.Id
			}, null);
		}

		[HttpPut()]
		[SwaggerResponse(200, typeof(OkObjectResult), Description = "The blog post has been successfully updated")]
		[SwaggerResponse(404, typeof(NotFoundResult), Description = "There was no blog post found by the provided blog post Id")]
		public IActionResult PutBlogPost([FromBody] BlogPostUpdateRequest blogPostUpdateRequest)
		{
			BlogPost existingBlogPost = _blogPostRepository.GetById(blogPostUpdateRequest.Id);
			
			if (existingBlogPost == null)
				return NotFound();

			existingBlogPost.Title = blogPostUpdateRequest.Title;
			existingBlogPost.Body = blogPostUpdateRequest.Body;
			existingBlogPost.Summary = blogPostUpdateRequest.Summary;
			existingBlogPost.UpdatedAt = DateTime.UtcNow;

			_blogPostRepository.Update(existingBlogPost);

			return Ok(existingBlogPost);
		}

		[HttpDelete()]
		[SwaggerResponse(200, typeof(NoContentResult), Description = "The blog post has been successfully deleted")]
		[SwaggerResponse(400, typeof(BadRequestResult), Description = "Please, provide valid Post Id")]
		[SwaggerResponse(404, typeof(NotFoundResult), Description = "There was no blog post found by the provided blog post Id")]
		public IActionResult DeleteBlogPost(long postId)
		{
			if (postId == 0)
				return BadRequest();
			
			var existingBlogPost = _blogPostRepository.GetById(postId);
			if (existingBlogPost == null)
				return NotFound();

			_blogPostRepository.Delete(existingBlogPost);

			return NoContent();
		}

		[HttpGet("{postId}/comments", Name = nameof(GetCommentsByPostId))]
		[ActionName(nameof(GetCommentsByPostId))]
		[SwaggerResponse(200, typeof(BlogPost), Description = "OK")]
		[SwaggerResponse(400, typeof(BadRequestResult), Description = "Please, provide valid Post Id")]
		[SwaggerResponse(404, typeof(NotFoundResult), Description = "There were no comments found by the provided blog post Id")]
		public IActionResult GetCommentsByPostId(long postId)
		{
			if (postId == 0)
				return BadRequest("Post Id must be greater then 0");

			ICollection<PostComment> comments = _commentRepository.GetCommentsWithReactionsByPostId(postId);

			if (!comments.Any())
				return NotFound();

			return Ok(comments);
		}
	}
}
