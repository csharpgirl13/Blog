using System.Collections.Generic;
using Blog.Model;

namespace Blog.Repository
{
	public interface ICommentRepository : IEntityRepository<PostComment>
	{
		ICollection<PostComment> GetCommentsWithReactionsByPostId(long postId);
	}
}
