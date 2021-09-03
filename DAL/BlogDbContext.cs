using System;
using Blog.Model;
using Microsoft.EntityFrameworkCore;

namespace Blog.DAL
{
	public class BlogDbContext: DbContext
	{
		public BlogDbContext(DbContextOptions<BlogDbContext> options) :base(options)
		{
			
		}
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var user1 = new User
			{
				Id = 1,
				FirstName = "Olivia",
				LastName = "Jones",
				UserName = "olivia.jones",
				Email = "Olivia.Jones@myblog.com",
				Intro = "Hello, I'm Olivia Jones and my blog is about traveling."
			};
			var user2 = new User
			{
				Id = 2,
				FirstName = "Smith",
				LastName = "Taylor",
				UserName = "smith.taylor",
				Email = "Smith.Taylor@behappy.com",
				Intro = "Hi everyone, I'm Smith Taylor from England. In my blog I will focus on fitness and healthy nutrition."
			};

			modelBuilder.Entity<User>().HasData(user1,user2);

			modelBuilder.Entity<BlogPost>()
				.HasOne(u => u.User);
			modelBuilder.Entity<BlogPost>()
				.HasMany(c => c.Comments);

			modelBuilder.Entity<BlogPost>().HasData(
				new 
				{
					Id = 1,
					Title = "My last trip to Formentera",
					Body = @"Formentera is undoubtedly one of the favorite destinations of the Mediterranean Sea. 
							The fourth largest island in the Balearic Islands, Formentera lies to the south of the island of Ibiza and gathers thousands of visitors every year in search of sun and beaches. 
							Although most of the services are only available in the summer season from the beginning of May to the end of October, more and more tourists discover the peace, tranquility and magic that this wonderful island offers in autumn and winter season.
							If you are already in Ibiza it takes about 45 minutes by boat to get there. It has 17 best known beaches. 
							But apart from all these, there are many small beaches and beautiful spots where you can enjoy the sun, sand and sea.",
					Summary = "This place is absolute worth visiting if you like crystal clear water and want to find peace of Hawaii in Europe",
					CreatedAt = DateTime.UtcNow,
					UserId = 1
				},
				new 
				{
					Id = 2,
					Title = "How to loose fat while preserving muscles",
					Body = @"If you’re looking to trim down, figuring out the secret sauce for saving muscle while losing fat can feel pretty overwhelming.

					The good news: While it’s not always easy,
					it’s totally possible to lose fat and gain muscles at the same time.It just takes a little patience and planning.Trust us,
					I have asked * a lot * of experts.",
					Summary = "We are not all the same, and the same approach will not work for everybody.",
					CreatedAt = DateTime.UtcNow,
					UserId = 2
				});

			modelBuilder.Entity<PostComment>()
				.HasOne(p => p.BlogPost);
			modelBuilder.Entity<PostComment>()
				.HasMany(r => r.Reactions);
			modelBuilder.Entity<PostReaction>()
				.HasOne(u => u.User);
		}
	}
}
