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
    }
}