using System.ComponentModel.DataAnnotations;
using Service.Core.Domain.Models.Education;

namespace Service.EducationApi.Models
{
	public class UseRetryRequest
	{
		[Required]
		public EducationTutorial Tutorial { get; set; }

		[Required]
		public int Unit { get; set; }

		[Required]
		public int Task { get; set; }
	}
}