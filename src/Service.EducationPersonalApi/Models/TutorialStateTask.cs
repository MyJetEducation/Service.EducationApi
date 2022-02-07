namespace Service.EducationPersonalApi.Models
{
	public class TutorialStateTask
	{
		public int Task { get; set; }

		public int TestScore { get; set; }

		public RetryInfo Retry { get; set; }
	}
}