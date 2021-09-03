namespace Blog.Contracts
{
	public class BlogPostUpdateCommentRequest
	{
		public long CommentId { get; set; }
		public long UserId { get; set; }
		public long PostId { get; set; }
		public string Content { get; set; }
	}
}
