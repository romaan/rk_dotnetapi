using Microsoft.EntityFrameworkCore;
using SimpleAPI.Models;

namespace SimpleAPI
{
	public class ApiContext : DbContext
	{
		public ApiContext(DbContextOptions<ApiContext> options)
			: base(options)
		{
		}

		public DbSet<TodoItem> TodoItems { get; set; }

	}
}