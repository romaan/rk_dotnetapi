using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleAPI.Models
{
	public class TodoItem
	{
        [Key]
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public bool? IsDone { get; set; }
	}
}
