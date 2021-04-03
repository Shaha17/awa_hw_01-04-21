using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DZ_01_04_21.Context;
using DZ_01_04_21.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DZ_01_04_21.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class QuoteController : ControllerBase
	{

		private readonly ILogger<QuoteController> _logger;
		private readonly QuoteDbContext _quoteDbContext;

		public QuoteController(ILogger<QuoteController> logger, QuoteDbContext quoteDbContext)
		{
			_quoteDbContext = quoteDbContext;
			_logger = logger;
		}

		[HttpGet]
		[Route("Get")]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _quoteDbContext.Quotes.ToListAsync());
		}

		[HttpGet]
		[Route("Get/{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			if (id <= 0)
			{
				return BadRequest();
			}
			var rez = await _quoteDbContext.Quotes.FindAsync(id);
			if (rez == null)
			{
				return NotFound();
			}
			return Ok(rez);
		}

		[HttpPost]
		[Route("Create")]
		public async Task<IActionResult> Create([FromBody] Quote model)
		{
			if (model == null)
			{
				return BadRequest();
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			model.InsertDate = DateTime.Now;

			_quoteDbContext.Quotes.Add(model);
			await _quoteDbContext.SaveChangesAsync();
			return Created("Quote created", model);
		}

		[HttpDelete]
		[Route("Delete/{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			if (id <= 0)
			{
				return BadRequest();
			}

			var rez = await _quoteDbContext.Quotes.FindAsync(id);

			_quoteDbContext.Quotes.Remove(rez);
			await _quoteDbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpPost]
		[Route("Edit/{id}")]
		public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] Quote model)
		{
			if (id <= 0)
			{
				return BadRequest();
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var rez = await _quoteDbContext.Quotes.FindAsync(id);

			if (rez == null)
			{
				return NotFound();
			}

			rez.Author = model.Author;
			rez.InsertDate = rez.InsertDate;
			rez.Text = rez.Text;

			_quoteDbContext.Quotes.Add(model);
			await _quoteDbContext.SaveChangesAsync();
			return Ok(model);
		}




	}
}
