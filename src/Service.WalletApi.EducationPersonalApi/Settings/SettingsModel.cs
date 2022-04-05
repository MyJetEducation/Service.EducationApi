using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.WalletApi.EducationPersonalApi.Settings
{
	public class SettingsModel
	{
		[YamlProperty("EducationPersonalApi.SeqServiceUrl")]
		public string SeqServiceUrl { get; set; }

		[YamlProperty("EducationPersonalApi.ZipkinUrl")]
		public string ZipkinUrl { get; set; }

		[YamlProperty("EducationPersonalApi.ElkLogs")]
		public LogElkSettings ElkLogs { get; set; }

		[YamlProperty("EducationPersonalApi.EnableApiTrace")]
		public bool EnableApiTrace { get; set; }

		[YamlProperty("EducationPersonalApi.MyNoSqlReaderHostPort")]
		public string MyNoSqlReaderHostPort { get; set; }

		[YamlProperty("EducationPersonalApi.AuthMyNoSqlReaderHostPort")]
		public string AuthMyNoSqlReaderHostPort { get; set; }

		[YamlProperty("EducationPersonalApi.SessionEncryptionKeyId")]
		public string SessionEncryptionKeyId { get; set; }

		[YamlProperty("EducationPersonalApi.MyNoSqlWriterUrl")]
		public string MyNoSqlWriterUrl { get; set; }

		[YamlProperty("EducationPersonalApi.EducationFlowServiceUrl")]
		public string EducationFlowServiceUrl { get; set; }

		[YamlProperty("EducationPersonalApi.UserRewardServiceUrl")]
		public string UserRewardServiceUrl { get; set; }
	}
}