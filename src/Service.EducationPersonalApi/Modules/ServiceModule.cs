using Autofac;
using Microsoft.Extensions.Logging;
using Service.Core.Client.Services;
using Service.TutorialPersonal.Client;
using Service.UserReward.Client;

namespace Service.EducationPersonalApi.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterTutorialPersonalClient(Program.Settings.EducationFlowServiceUrl, Program.LogFactory.CreateLogger(typeof(TutorialPersonalClientFactory)));
			builder.RegisterUserRewardClient(Program.Settings.UserRewardServiceUrl);

			builder.RegisterType<SystemClock>().AsImplementedInterfaces().SingleInstance();

			builder.Register(context => new EncoderDecoder(Program.EncodingKey))
				.As<IEncoderDecoder>()
				.SingleInstance();
		}
	}
}