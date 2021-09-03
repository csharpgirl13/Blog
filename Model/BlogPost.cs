using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Model
{
	public class BlogPost : BaseEntity
	{
		[Required]
		public string Title { get; set; }
		public string Summary { get; set; }
		[Required]
		public string Body { get; set; }
		[Required]
		public User User { get; set; }
		[Required]
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public ICollection<PostComment> Comments { get; set; }
	}
}
