using System;
namespace DZ_01_04_21.Models
{
	public class Quote
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public string Author { get; set; }
		public DateTime InsertDate { get; set; }
	}
}