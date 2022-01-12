using System.Collections.Generic;

namespace Service.EducationApi.Models.TaskModels
{
	public class PersonalStateUnit
	{
		public int Unit { get; set; }

		public int TestScore { get; set; }

		public IEnumerable<PersonalStateTask> Tasks { get; set; }

		public int HabitCount { get; set; }

		public int SkillCount { get; set; }
	}
}