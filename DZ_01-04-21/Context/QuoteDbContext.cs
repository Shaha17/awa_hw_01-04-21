using Microsoft.EntityFrameworkCore;
using DZ_01_04_21.Models;

namespace DZ_01_04_21.Context
{
	public class QuoteDbContext : DbContext
	{
		public QuoteDbContext(DbContextOptions options) : base(options)
		{
		}
		public QuoteDbContext()
		{
		}
		public DbSet<Quote> Quotes { get; set; }
	}
}