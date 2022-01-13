namespace Service.EducationApi.Models.TaskModels
{
	public class PersonalStateTask
	{
		public int Task { get; set; }

		public int TestScore { get; set; }

		public RetryInfo Retry { get; set; }
	}
}