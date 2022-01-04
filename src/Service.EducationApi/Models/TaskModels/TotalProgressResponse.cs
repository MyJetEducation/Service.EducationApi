namespace Service.EducationApi.Models.TaskModels
{
	public class TotalProgressResponse
	{
		public int HabitProgress { get; set; }

		public string HabitName { get; set; }

		public int SkillProgress { get; set; }

		public string SkillName { get; set; }

		public string[] Achievements { get; set; }
	}
}