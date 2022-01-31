using System.Collections.Generic;

namespace Service.EducationPersonalApi.Models
{
	public class PersonalStateResponse
	{
		public bool Available { get; set; }

		public IEnumerable<PersonalStateUnit> Units { get; set; }

		public TotalProgressResponse TotalProgress { get; set; }
	}
}