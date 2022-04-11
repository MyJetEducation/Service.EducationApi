using System.ComponentModel.DataAnnotations;

namespace Service.WalletApi.EducationPersonalApi.Controllers.Contracts
{
	public class TaskAnswer
	{
		[Required]
		public int Number { get; set; }

		[Required]
		public int[] Value { get; set; }
	}
}