using System.ServiceModel;
using System.Threading.Tasks;
using Service.EducationApi.Grpc.Models;

namespace Service.EducationApi.Grpc
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        Task<HelloMessage> SayHelloAsync(HelloRequest request);
    }
}