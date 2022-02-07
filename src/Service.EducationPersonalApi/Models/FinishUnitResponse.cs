namespace Service.EducationPersonalApi.Models
{
	public class FinishUnitResponse
	{
		public TutorialStateUnit Unit { get; set; }

		public int TrueFalseProgress { get; set; }

		public int CaseProgress { get; set; }
	}
}