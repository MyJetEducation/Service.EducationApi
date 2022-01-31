namespace Service.EducationPersonalApi.Models
{
	public class FinishUnitResponse
	{
		public PersonalStateUnit Unit { get; set; }

		public TotalProgressResponse TotalProgress { get; set; }
	}
}