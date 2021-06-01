using System.Collections.Generic;

namespace SavoryTreats.Models
{
	public class Flavor
	{
		public int FlavorId { get; set; }
		public string Name { get; set; }
		public virtual ICollection<FlavorTreat> Treats { get; }

		public Flavor()
		{
			this.Treats = new HashSet<FlavorTreat>();
		}
	}
}