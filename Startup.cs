using Blog.DAL;
using Blog.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Blog
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<BlogDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("cs")));
			
			services.AddSwaggerDocument(d =>
			{
				d.PostProcess = doc =>
				{
					doc.Info.Title = "Blog API";
				};
			});

			services.AddTransient(typeof(IBlogPostRepository), typeof(BlogPostRepository));
			services.AddTransient(typeof(ICommentRepository), typeof(CommentRepository));
			services.AddTransient(typeof(IEntityRepository<>), typeof(EntityRepository<>));
			services.AddControllers().AddNewtonsoftJson(options =>
			{
				options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				options.SerializerSettings.Converters.Add(new StringEnumConverter());
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseOpenApi();
			app.UseSwaggerUi3();

			using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
			if (serviceScope != null)
			{
				var context = serviceScope.ServiceProvider.GetRequiredService<BlogDbContext>();
				context.Database.EnsureCreated();
				context.Database.Migrate();
			}
		}
	}
}
