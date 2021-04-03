using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DZ_01_04_21.Context;
using Microsoft.EntityFrameworkCore;

namespace DZ_01_04_21
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				try
				{
					var services = scope.ServiceProvider;
					var context = services.GetRequiredService<QuoteDbContext>();
					context.Database.Migrate();
				}
				catch (Exception ex)
				{
					System.Console.WriteLine("Migration error:\n" + ex.Message);
				}
			}

			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
