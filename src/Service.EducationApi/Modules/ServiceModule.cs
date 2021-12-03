using Autofac;
using Service.EducationApi.Services;
using Service.KeyValue.Client;
using Service.UserInfo.Crud.Client;
using Service.UserInfo.Crud.Grpc;

namespace Service.EducationApi.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterUserInfoCrudClient(Program.Settings.UserInfoCrudServiceUrl);
			builder.RegisterKeyValueClient(Program.Settings.KeyValueServiceUrl);

			builder.Register(context =>
				new TokenService(
					context.Resolve<IUserInfoService>(),
					Program.JwtSecret,
					Program.Settings.JwtTokenExpireMinutes,
					Program.Settings.RefreshTokenExpireMinutes))
				.As<ITokenService>()
				.SingleInstance();
		}
	}
}