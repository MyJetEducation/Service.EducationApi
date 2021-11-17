using Autofac;
using Service.EducationApi.Grpc;

// ReSharper disable UnusedMember.Global

namespace Service.EducationApi.Client
{
    public static class AutofacHelper
    {
        public static void RegisterEducationApiClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new EducationApiClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetHelloService()).As<IHelloService>().SingleInstance();
        }
    }
}
