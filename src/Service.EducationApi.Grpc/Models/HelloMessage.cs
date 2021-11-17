using System.Runtime.Serialization;
using Service.EducationApi.Domain.Models;

namespace Service.EducationApi.Grpc.Models
{
    [DataContract]
    public class HelloMessage : IHelloMessage
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}