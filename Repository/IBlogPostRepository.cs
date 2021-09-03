using System.Collections.Generic;
using Blog.Model;

namespace Blog.Repository
{
	public interface IBlogPostRepository : IEntityRepository<BlogPost>
	{
		ICollection<BlogPost> GetAllPostsByUserId(long userId);
	}
}
