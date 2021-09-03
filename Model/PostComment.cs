using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Model
{
	public class PostComment : BaseEntity
	{
		[Required]
		public User User { get; set; }
		[Required]
		public BlogPost BlogPost { get; set; }
		[Required]
		public string Content { get; set; }
		[Required]
		public DateTime CreatedAt { get; set; }
		public ICollection<PostReaction> Reactions { get; set; }
	}
}
