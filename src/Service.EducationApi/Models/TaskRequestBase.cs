using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Service.EducationApi.Models
{
	public class TaskRequestBase
	{
		[Required]
		public bool IsRetry { get; set; }

		[Required]
		[Description("Milliseconds")]
		public int Duration { get; set; }
	}
}