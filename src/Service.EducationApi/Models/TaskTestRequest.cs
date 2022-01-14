using System.ComponentModel.DataAnnotations;

namespace Service.EducationApi.Models
{
	public class TaskTestRequest: TaskRequestBase
	{
		[Required]
		public TaskAnswer[] Answers { get; set; }
	}
}