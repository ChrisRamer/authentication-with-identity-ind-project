using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SavoryTreats.Models
{
	public class SavoryTreatsContextFactory : IDesignTimeDbContextFactory<SavoryTreatsContext>
	{

		SavoryTreatsContext IDesignTimeDbContextFactory<SavoryTreatsContext>.CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
			  .SetBasePath(Directory.GetCurrentDirectory())
			  .AddJsonFile("appsettings.json")
			  .Build();

			var builder = new DbContextOptionsBuilder<SavoryTreatsContext>();
			string connectionString = configuration.GetConnectionString("DefaultConnection");

			builder.UseMySql(connectionString);

			return new SavoryTreatsContext(builder.Options);
		}
	}
}