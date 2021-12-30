using System.ComponentModel.DataAnnotations;

namespace Service.EducationApi.Models.TaskModels
{
	public class TaskCaseRequest: TaskRequestBase
	{
		[Required]
		public int Value { get; set; }
	}
}