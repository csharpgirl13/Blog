namespace Blog.Contracts
{
	public class BlogPostAddRequest
	{
		public string Title { get; set; }
		public string Summary { get; set; }
		public string Body { get; set; }
		public long UserId { get; set; }
	}
}
