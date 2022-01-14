using System.ComponentModel.DataAnnotations;

namespace Service.EducationApi.Models
{
	public class TaskCaseRequest: TaskRequestBase
	{
		[Required]
		public int Value { get; set; }
	}
}