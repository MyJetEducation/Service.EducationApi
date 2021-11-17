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
    }
}
