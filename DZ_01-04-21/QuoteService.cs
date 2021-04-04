using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using DZ_01_04_21.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DZ_01_04_21
{
	public class QuoteService : IHostedService
	{

		Timer _timer;
		IServiceProvider serviceProvider;

		public QuoteService(IServiceProvider service)
		{
			serviceProvider = service;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{

			_timer = new Timer(new TimerCallback(CheckQuotesDate), null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
			return Task.CompletedTask;
		}
		public async Task StopAsync(CancellationToken cancellationToken)
		{
			await _timer.DisposeAsync();
		}

		public void CheckQuotesDate(object obj)
		{
			// System.Console.WriteLine(DateTime.Now.ToShortTimeString());
			using (var scope = serviceProvider.CreateScope())
			{
				try
				{
					var services = scope.ServiceProvider;
					var context = services.GetRequiredService<QuoteDbContext>();
					var lst = context.Quotes.ToList();
					var deleteLst = lst.Where(predicate => (DateTime.Now - predicate.InsertDate) > TimeSpan.FromDays(31));
					foreach (var item in deleteLst)
					{
						System.Console.WriteLine(item.Text + " " + item.InsertDate.ToShortTimeString());
					}
					context.RemoveRange(deleteLst);
					context.SaveChanges();

				}
				catch (Exception ex)
				{
					System.Console.WriteLine("CheckQuoteDate service error: " + ex.Message);
				}
			}
		}
	}
}