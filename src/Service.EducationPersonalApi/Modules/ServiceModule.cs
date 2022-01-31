using Autofac;
using Service.TutorialPersonal.Client;
using Service.UserInfo.Crud.Client;
using Service.UserReward.Client;

namespace Service.EducationPersonalApi.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterUserInfoCrudClient(Program.Settings.UserInfoCrudServiceUrl);
			builder.RegisterTutorialPersonalClient(Program.Settings.TutorialPersonalServiceUrl);
			builder.RegisterUserRewardClient(Program.Settings.UserRewardServiceUrl);
		}
	}
}