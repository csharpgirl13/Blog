using Blog.Model;

namespace Blog.Contracts
{
	public class CommentReactionRequest
	{
		public long CommentId { get; set; }
		public Reaction Reaction { get; set; }
		public long UserId { get; set; }
	}
}
