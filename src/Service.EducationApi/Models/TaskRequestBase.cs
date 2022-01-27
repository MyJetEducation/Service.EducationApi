using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Service.EducationApi.Models
{
	public class TaskRequestBase
	{
		[Required]
		[DefaultValue(false)]
		public bool IsRetry { get; set; }

		[Required]
		[Range(1, int.MaxValue)]
		[DefaultValue(0)]
		[Description("Milliseconds")]
		public int Duration { get; set; }
	}
}