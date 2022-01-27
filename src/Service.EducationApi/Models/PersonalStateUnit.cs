using System.Collections.Generic;

namespace Service.EducationApi.Models
{
	public class PersonalStateUnit
	{
		public int Unit { get; set; }

		public int TestScore { get; set; }

		public IEnumerable<PersonalStateTask> Tasks { get; set; }
	}
}