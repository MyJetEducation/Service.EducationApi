using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Service.WalletApi.EducationPersonalApi.Controllers.Contracts
{
	public class TutorialStateUnit
	{
		[Range(1, 5)]
		public int Unit { get; set; }

		[Range(1, 100)]
		public int TaskScore { get; set; }

		[Range(1, 6)]
		public IEnumerable<TutorialStateTask> Tasks { get; set; }
	}
}