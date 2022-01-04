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

		[YamlProperty("EducationApi.JwtTokenExpireMinutes")]
		public int JwtTokenExpireMinutes { get; set; }

		[YamlProperty("EducationApi.RefreshTokenExpireMinutes")]
		public int RefreshTokenExpireMinutes { get; set; }

		[YamlProperty("EducationApi.UserInfoCrudServiceUrl")]
		public string UserInfoCrudServiceUrl { get; set; }

		[YamlProperty("EducationApi.KeyValueServiceUrl")]
		public string KeyValueServiceUrl { get; set; }

		[YamlProperty("EducationApi.UserProfileServiceUrl")]
		public string UserProfileServiceUrl { get; set; }

		[YamlProperty("EducationApi.PasswordRecoveryServiceUrl")]
		public string PasswordRecoveryServiceUrl { get; set; }

		[YamlProperty("EducationApi.RegistrationServiceUrl")]
		public string RegistrationServiceUrl { get; set; }

		[YamlProperty("EducationApi.TutorialPersonalServiceUrl")]
		public string TutorialPersonalServiceUrl { get; set; }

		[YamlProperty("EducationApi.JwtAudience")]
		public string JwtAudience { get; set; }
	}
}