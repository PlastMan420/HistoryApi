using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryMockApi.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		public string Details { get; set; }
		public DateTime Time { get; set; }
		public string ImageUrl { get; set; }
	}
}
