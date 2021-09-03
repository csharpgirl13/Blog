using System;
using System.Collections.Generic;
using System.Linq;
using Blog.DAL;
using Blog.Model;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository
{
	public class BlogPostRepository : EntityRepository<BlogPost>, IBlogPostRepository
	{
		public BlogPostRepository(BlogDbContext blogDbContext) :base(blogDbContext)
		{
		}

		public ICollection<BlogPost> GetAllPostsByUserId(long userId)
		{
			if (userId == 0)
			{
				throw new ArgumentNullException($"No User Id has been provided");
			}
			return Entities.Where(p => p.User.Id == userId).Include(c=>c.Comments).ToList();
		}

	}
}
