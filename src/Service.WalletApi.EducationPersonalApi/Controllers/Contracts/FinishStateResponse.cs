using System.ComponentModel.DataAnnotations;
using Service.Core.Client.Constants;

namespace Service.WalletApi.EducationPersonalApi.Controllers.Contracts
{
	public class FinishStateResponse
	{
		public int Test { get; set; }

		public int TrueFalse { get; set; }

		public int Case { get; set; }

		public int Game { get; set; }

		public int Video { get; set; }

		public int Text { get; set; }

		[EnumDataType(typeof (UserAchievement))]
		public UserAchievement[] Achievements { get; set; }
	}
}