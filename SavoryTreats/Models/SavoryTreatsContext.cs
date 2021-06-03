using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SavoryTreats.Models
{
	public class SavoryTreatsContext : IdentityDbContext<ApplicationUser>
	{
		public virtual DbSet<Flavor> Flavors { get; set; }
		public virtual DbSet<Treat> Treats { get; set; }
		public virtual DbSet<FlavorTreat> FlavorTreats { get; set; }

		public SavoryTreatsContext(DbContextOptions options) : base(options) { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLazyLoadingProxies();
		}
	}
}