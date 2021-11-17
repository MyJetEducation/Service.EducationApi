using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;
using Service.EducationApi.Grpc;

namespace Service.EducationApi.Client
{
    [UsedImplicitly]
    public class EducationApiClientFactory: MyGrpcClientFactory
    {
        public EducationApiClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IHelloService GetHelloService() => CreateGrpcService<IHelloService>();
    }
}
