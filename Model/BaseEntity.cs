using System.ComponentModel.DataAnnotations;

namespace Blog.Model
{
	public class BaseEntity
	{
		[Key]
		[Required]
		public int Id { get; set; }
	}
}
