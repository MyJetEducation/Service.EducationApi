using System.ComponentModel.DataAnnotations;

namespace Service.EducationApi.Models.TaskModels
{
	public class TaskTrueFalseRequest: TaskRequestBase
	{
		[Required]
		public TaskTrueFalse[] Answers { get; set; }
	}
}