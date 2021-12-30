using System;
using System.Collections.Generic;

namespace Service.EducationApi.Models.TaskModels
{
	public class PersonalStateResponse
	{
		public bool Available { get; set; }

		public TimeSpan Duration { get; set; }

		public IEnumerable<PersonalStateUnit> Units { get; set; }
	}
}