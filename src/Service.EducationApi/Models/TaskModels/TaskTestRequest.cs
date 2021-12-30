using System.ComponentModel.DataAnnotations;

namespace Service.EducationApi.Models.TaskModels
{
	public class TaskTestRequest: TaskRequestBase
	{
		[Required]
		public TaskAnswer[] Answers { get; set; }
	}
}