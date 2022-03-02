using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.EducationPersonalApi.Settings
{
	public class SettingsModel
	{
		[YamlProperty("EducationPersonalApi.SeqServiceUrl")]
		public string SeqServiceUrl { get; set; }

		[YamlProperty("EducationPersonalApi.ZipkinUrl")]
		public string ZipkinUrl { get; set; }

		[YamlProperty("EducationPersonalApi.ElkLogs")]
		public LogElkSettings ElkLogs { get; set; }

		[YamlProperty("EducationPersonalApi.TutorialPersonalServiceUrl")]
		public string TutorialPersonalServiceUrl { get; set; }

		[YamlProperty("EducationPersonalApi.UserRewardServiceUrl")]
		public string UserRewardServiceUrl { get; set; }
	}
}