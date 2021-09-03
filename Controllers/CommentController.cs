using System;
using Blog.Contracts;
using Blog.Model;
using Blog.Repository;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Blog.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CommentController : ControllerBase
	{
		private readonly IEntityRepository<BlogPost> _entityPostRepository;
		private readonly IEntityRepository<User> _userRepository;
		private readonly IEntityRepository<PostComment> _commentRepository;
		public CommentController(IEntityRepository<BlogPost> entityPostRepository, IEntityRepository<User> userRepository, IEntityRepository<PostComment> commentRepository)
		{
			_entityPostRepository = entityPostRepository;
			_userRepository = userRepository;
			_commentRepository = commentRepository;
		}

		[HttpPost()]
		[SwaggerResponse(200, typeof(OkObjectResult), Description = "OK")]
		[SwaggerResponse(400, typeof(BadRequestResult), Description = "Content is empty")]
		public IActionResult PostNewComment([FromBody] BlogPostAddCommentRequest postUpdateCommentRequest)
		{
			var blogPost = _entityPostRepository.GetById(postUpdateCommentRequest.PostId);
			var user = _userRepository.GetById(postUpdateCommentRequest.UserId);

			if (string.IsNullOrWhiteSpace(postUpdateCommentRequest.Content))
				return BadRequest();

			PostComment postComment = new PostComment
			{
				Content = postUpdateCommentRequest.Content,
				BlogPost = blogPost,
				CreatedAt = DateTime.UtcNow,
				User = user
			};

			var comment = _commentRepository.Insert(postComment);

			return Ok(comment);
		}

		[HttpPut()]
		[SwaggerResponse(200, typeof(OkObjectResult), Description = "OK")]
		[SwaggerResponse(400, typeof(NotFoundResult), Description = "Comment was not found")]
		public IActionResult PutComment([FromBody] BlogPostUpdateCommentRequest postUpdateCommentRequest)
		{
			var existingComment = _commentRepository.GetById(postUpdateCommentRequest.CommentId);
			
			if (existingComment == null)
				return NotFound();

			if (existingComment.Content != postUpdateCommentRequest.Content)
			{
				existingComment.Content = postUpdateCommentRequest.Content;
				_commentRepository.Update(existingComment);
			}
			
			return Ok(existingComment);
		}

		[HttpDelete()]
		[SwaggerResponse(204, typeof(NoContentResult), Description = "OK")]
		[SwaggerResponse(400, typeof(NotFoundResult), Description = "Comment was not found")]
		public IActionResult DeleteComment(long commentId)
		{
			var existingComment = _commentRepository.GetById(commentId);
			if (existingComment == null)
				return NotFound();

			_commentRepository.Delete(existingComment);

			return NoContent();
		}
	}
}
