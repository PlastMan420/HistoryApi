using HistoryMockApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryMockApi.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) // initialize EF Core DbContext. class must extend DbContext
		{

		}
		public DbSet<UserModel> Users { get; set; }
	}
}
