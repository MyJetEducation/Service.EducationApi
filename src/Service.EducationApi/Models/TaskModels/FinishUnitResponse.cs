namespace Service.EducationApi.Models.TaskModels
{
	public class FinishUnitResponse
	{
		public PersonalStateUnit Unit { get; set; }

		public TotalProgressResponse TotalProgress { get; set; }
	}
}