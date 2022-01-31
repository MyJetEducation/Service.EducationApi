using System.ComponentModel.DataAnnotations;

namespace Service.EducationPersonalApi.Models
{
	public class TaskCaseRequest: TaskRequestBase
	{
		[Required]
		public int Value { get; set; }
	}
}