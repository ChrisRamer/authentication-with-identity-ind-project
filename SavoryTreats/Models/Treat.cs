using System.Collections.Generic;

namespace SavoryTreats.Models
{
	public class Treat
	{
		public int TreatId { get; set; }
		public string Name { get; set; }
		public virtual ApplicationUser User { get; set; }
		public virtual ICollection<FlavorTreat> Flavors { get; }

		public Treat()
		{
			this.Flavors = new HashSet<FlavorTreat>();
		}
	}
}