namespace Service.EducationApi.Models
{
	public class TotalProgressResponse
	{
		public int HabitProgress { get; set; }

		public int HabitValue { get; set; }

		public int SkillProgress { get; set; }

		public int SkillValue { get; set; }

		public string[] Achievements { get; set; }
	}
}