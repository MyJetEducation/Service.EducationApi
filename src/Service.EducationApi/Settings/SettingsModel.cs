using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.EducationApi.Settings
{
	public class SettingsModel
	{
		[YamlProperty("EducationApi.SeqServiceUrl")]
		public string SeqServiceUrl { get; set; }

		[YamlProperty("EducationApi.ZipkinUrl")]
		public string ZipkinUrl { get; set; }

		[YamlProperty("EducationApi.ElkLogs")]
		public LogElkSettings ElkLogs { get; set; }

		[YamlProperty("EducationApi.UserInfoCrudServiceUrl")]
		public string UserInfoCrudServiceUrl { get; set; }

		[YamlProperty("EducationApi.TutorialPersonalServiceUrl")]
		public string TutorialPersonalServiceUrl { get; set; }

		[YamlProperty("EducationApi.JwtAudience")]
		public string JwtAudience { get; set; }

		[YamlProperty("EducationApi.UserRewardServiceUrl")]
		public string UserRewardServiceUrl { get; set; }
	}
}