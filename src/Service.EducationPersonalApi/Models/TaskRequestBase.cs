using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Service.EducationPersonalApi.Models
{
	public class TaskRequestBase
	{
		[DefaultValue(false)]
		public bool IsRetry { get; set; }

		[Required]
		[Description("Token from TimeLogger service")]
		public string TimeToken { get; set; }
	}
}