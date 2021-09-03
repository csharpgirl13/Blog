using System;
using System.Linq;
using Blog.DAL;
using Blog.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Blog.Repository
{
	public class EntityRepository<T> : IEntityRepository<T> where T : BaseEntity
	{
		protected BlogDbContext Context { get; }
		protected DbSet<T> Entities { get; }

		public EntityRepository(BlogDbContext blogDbContext)
		{
			Context = blogDbContext;
			Entities = blogDbContext.Set<T>();
		}

		public T GetById(long id)
		{
			if (id == 0)
			{
				throw new ArgumentNullException($"No entity id has been provided");
			}
			return Entities.FirstOrDefault(e => e.Id == id);
		}

		public T Insert(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException($"No entity typeof {typeof(T).Name} was not provided.");
			}

			EntityEntry<T> result = Entities.Add(entity);
			Context.SaveChanges();

			return result.Entity;
		}

		public void Update(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException($"No entity typeof {typeof(T).Name} was not provided.");
			}

			Context.Update(entity);
			Context.SaveChanges();
		}

		public void Delete(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException($"No entity typeof {typeof(T).Name} was not provided.");
			}
			Entities.Remove(entity);
			Context.SaveChanges();
		}
	}
}
