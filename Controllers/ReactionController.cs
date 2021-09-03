using Blog.Contracts;
using Blog.Model;
using Blog.Repository;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Blog.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ReactionController : ControllerBase
	{
		private readonly IEntityRepository<PostReaction> _reactionRepository;
		private readonly IEntityRepository<PostComment> _commentRepository;
		private readonly IEntityRepository<User> _userRepository;
		public ReactionController(IEntityRepository<PostReaction> reactionRepository, IEntityRepository<PostComment> commentRepository, IEntityRepository<User> userRepository)
		{
			_reactionRepository = reactionRepository;
			_commentRepository = commentRepository;
			_userRepository = userRepository;
		}

		[HttpPost()]
		[SwaggerResponse(200, typeof(OkObjectResult), Description = "OK")]
		[SwaggerResponse(400, typeof(BadRequestResult), Description = "Please, provide valid reaction request")]
		public IActionResult PostNewReaction([FromBody] CommentReactionRequest reactionRequest)
		{
			if (reactionRequest == null)
				return BadRequest();

			var comment = _commentRepository.GetById(reactionRequest.CommentId);
			var user = _userRepository.GetById(reactionRequest.UserId);

			var reaction = new PostReaction
			{
				Comment = comment,
				User = user,
				Reaction = reactionRequest.Reaction
			};

			var newReaction = _reactionRepository.Insert(reaction);
			
			return Ok(newReaction);
		}

		[HttpDelete()]
		[SwaggerResponse(204, typeof(NoContentResult), Description = "OK")]
		[SwaggerResponse(400, typeof(BadRequestResult), Description = "Please, provide reaction id to be deleted")]
		[SwaggerResponse(404, typeof(BadRequestResult), Description = "Reaction was not found")]
		public IActionResult DeleteReaction(long id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var reaction = _reactionRepository.GetById(id);

			if (reaction == null)
				return NotFound();

			_reactionRepository.Delete(reaction);

			return NoContent();
		}

	}
}
