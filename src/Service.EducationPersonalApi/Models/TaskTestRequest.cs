using System.ComponentModel.DataAnnotations;

namespace Service.EducationPersonalApi.Models
{
	public class TaskTestRequest: TaskRequestBase
	{
		[Required]
		public TaskAnswer[] Answers { get; set; }
	}
}