namespace Blog.Contracts
{
	public class BlogPostAddCommentRequest
	{
		public long UserId { get; set; }
		public long PostId { get; set; }
		public string Content { get; set; }
	}
}
