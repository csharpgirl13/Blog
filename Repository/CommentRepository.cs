using System;
using System.Collections.Generic;
using System.Linq;
using Blog.DAL;
using Blog.Model;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository
{
	public class CommentRepository : EntityRepository<PostComment>, ICommentRepository
	{
		public CommentRepository(BlogDbContext blogDbContext) : base(blogDbContext)
		{
		}

		public ICollection<PostComment> GetCommentsWithReactionsByPostId(long postId)
		{
			if (postId == 0)
			{
				throw new ArgumentNullException($"No blog post id has been provided");
			}

			return Entities.Where(u => u.BlogPost.Id == postId).Include(r => r.Reactions).Include(u => u.User).ToList();
		}
	}
}
