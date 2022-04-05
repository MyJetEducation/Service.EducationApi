using Autofac;
using Microsoft.Extensions.Logging;
using MyJetWallet.ApiSecurityManager.Autofac;
using MyJetWallet.Sdk.RestApiTrace;
using MyJetWallet.Sdk.Service;
using Service.Core.Client.Services;
using Service.TutorialPersonal.Client;
using Service.UserReward.Client;

namespace Service.WalletApi.EducationPersonalApi.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			// second parameter is null because we do not store api keys yet for wallet api
			builder.RegisterEncryptionServiceClient(ApplicationEnvironment.AppName, () => Program.Settings.MyNoSqlWriterUrl);

			builder.RegisterTutorialPersonalClient(Program.Settings.EducationFlowServiceUrl, Program.LoggerFactory.CreateLogger(typeof (TutorialPersonalClientFactory)));
			builder.RegisterUserRewardClient(Program.Settings.UserRewardServiceUrl);

			builder.RegisterType<SystemClock>().AsImplementedInterfaces().SingleInstance();

			builder.Register(context => new EncoderDecoder(Program.EncodingKey))
				.As<IEncoderDecoder>()
				.SingleInstance();

			if (Program.Settings.EnableApiTrace)
			{
				builder
					.RegisterInstance(new ApiTraceManager(Program.Settings.ElkLogs, "api-trace",
						Program.LoggerFactory.CreateLogger("ApiTraceManager")))
					.As<IApiTraceManager>()
					.As<IStartable>()
					.AutoActivate()
					.SingleInstance();
			}
		}
	}
}