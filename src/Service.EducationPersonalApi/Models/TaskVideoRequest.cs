using System.ComponentModel.DataAnnotations;

namespace Service.EducationPersonalApi.Models
{
	public class TaskVideoRequest : TaskRequestBase
	{
		[Required]
		public bool Passed { get; set; }
	}
}