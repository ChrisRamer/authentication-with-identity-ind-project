using Microsoft.EntityFrameworkCore;

namespace SavoryTreats.Models
{
	public class SavoryTreatsContext : DbContext
	{
		//public virtual DbSet<SavoryTreats> XXX_Model1Name { get; set; }
		//public virtual DbSet<SavoryTreats> XXX_Model2Name { get; set; }

		public SavoryTreatsContext(DbContextOptions options) : base(options) { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLazyLoadingProxies();
		}
	}
}