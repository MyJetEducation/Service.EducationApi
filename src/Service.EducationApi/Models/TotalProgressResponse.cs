namespace Service.EducationApi.Models
{
	public class TotalProgressResponse
	{
		public TotalProgressItemResponse Habit { get; set; }

		public TotalProgressItemResponse Skill { get; set; }

		public string[] Achievements { get; set; }
	}
}