using HistoryMockApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryMockApi.Data
{
	public class Seed
	{
		private readonly DataContext _context;
		public Seed(DataContext context)
		{
			_context = context;
		}
        public void SeedUsers()
        {
            var userData = System.IO.File.ReadAllText("Data/MOCK_DATA.json");
            var users = JsonConvert.DeserializeObject<List<UserModel>>(userData);
            foreach (var user in users)
            {
                _context.Users.Add(user);
            }

            _context.SaveChanges();
        }
    }
}
