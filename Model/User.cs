using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Model
{
	public class User : BaseEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Intro { get; set; }
		[Required]
		public string  UserName{ get; set; }
		[Required]
		public string  Email{ get; set; }
		public DateTime? DateOfBirth{ get; set; }
	}
}
