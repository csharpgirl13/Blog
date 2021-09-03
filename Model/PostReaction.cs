using System.ComponentModel.DataAnnotations;

namespace Blog.Model
{
	public class PostReaction : BaseEntity
	{
		[Required]
		public User User { get; set; }
		[Required]
		public PostComment Comment { get; set; }
		[Required]
		public Reaction Reaction { get; set; }
	}
}
