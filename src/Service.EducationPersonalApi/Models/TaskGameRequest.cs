using System.ComponentModel.DataAnnotations;

namespace Service.EducationPersonalApi.Models
{
	public class TaskGameRequest: TaskRequestBase
	{
		[Required]
		public bool Passed { get; set; }
	}
}