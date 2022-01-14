using System;
using System.ComponentModel.DataAnnotations;

namespace Service.EducationApi.Models
{
	public class TaskRequestBase
	{
		[Required]
		public bool IsRetry { get; set; }

		[Required]
		public TimeSpan Duration { get; set; }
	}
}