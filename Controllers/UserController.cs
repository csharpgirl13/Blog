using Blog.Contracts;
using Blog.Model;
using Blog.Repository;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Blog.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IEntityRepository<User> _userRepository;
		public UserController(IEntityRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}

		[HttpPost("Create")]
		[SwaggerResponse(200, typeof(OkObjectResult), Description = "OK")]
		[SwaggerResponse(400, typeof(BadRequestResult), Description = "Please, provide valid user request")]
		public IActionResult PostNewReaction([FromBody] UserRequest userRequest)
		{
			if (userRequest == null)
				return BadRequest();

			var user = new User
			{
				FirstName = userRequest.FirstName,
				LastName = userRequest.LastName,
				Intro = userRequest.Intro,
				UserName = userRequest.UserName,
				Email = userRequest.Email,
				DateOfBirth = userRequest.DateOfBirth
			};
			
			var newUser =_userRepository.Insert(user);
			
			return Ok(newUser);
		}

	}
}
