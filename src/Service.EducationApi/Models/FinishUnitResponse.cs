namespace Service.EducationApi.Models
{
	public class FinishUnitResponse
	{
		public PersonalStateUnit Unit { get; set; }

		public TotalProgressResponse TotalProgress { get; set; }
	}
}