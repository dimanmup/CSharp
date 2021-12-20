using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GrpcService_Test.Services
{
    public class HelloService : Hello.HelloBase
    {
        private readonly ILogger<HelloService> _logger;
        public HelloService(ILogger<HelloService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = (request.Age > 25 ? "Hello, " : "Hi, ") + request.Name,
                Status = 400
            });
        }
    }
}
