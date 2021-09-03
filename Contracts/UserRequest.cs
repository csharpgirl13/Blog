using System;

namespace Blog.Contracts
{
	public class UserRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Intro { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public DateTime DateOfBirth { get; set; }
	}
}
