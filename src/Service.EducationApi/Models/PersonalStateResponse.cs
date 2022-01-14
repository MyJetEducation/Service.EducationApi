using System.Collections.Generic;

namespace Service.EducationApi.Models
{
	public class PersonalStateResponse
	{
		public bool Available { get; set; }

		public IEnumerable<PersonalStateUnit> Units { get; set; }

		public TotalProgressResponse TotalProgress { get; set; }
	}
}