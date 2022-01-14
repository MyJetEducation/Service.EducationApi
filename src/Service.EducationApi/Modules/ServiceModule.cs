using Autofac;
using Service.TutorialPersonal.Client;
using Service.UserInfo.Crud.Client;

namespace Service.EducationApi.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterUserInfoCrudClient(Program.Settings.UserInfoCrudServiceUrl);
			builder.RegisterTutorialPersonalClient(Program.Settings.TutorialPersonalServiceUrl);
		}
	}
}