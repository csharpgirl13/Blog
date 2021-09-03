using Blog.Model;

namespace Blog.Repository
{
	public interface IEntityRepository<T> where T : BaseEntity
	{
		T GetById(long id);
		T Insert(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}
