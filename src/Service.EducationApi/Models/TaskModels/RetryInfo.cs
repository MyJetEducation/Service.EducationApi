namespace Service.EducationApi.Models.TaskModels
{
	public class RetryInfo
	{
		public bool InRetry { get; set; }

		public bool CanRetryByTime { get; set; }

		public bool CanRetryByCount { get; set; }
	}
}