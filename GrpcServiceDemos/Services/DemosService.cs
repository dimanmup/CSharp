using Grpc.Core;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServiceDemos
{
    public class DemosService 
        : Demos.DemosBase
    {
        public override Task<FieldsWithRulesReply> FieldsWithRules(FieldsWithRulesRequest request, ServerCallContext context)
        {
            return Task.FromResult(new FieldsWithRulesReply
            {
                Sum = request.Req + request.Opt + request.Rep.Sum()
            });
        }

        public override Task<EnumsHandlerReply> EnumsHandler(EnumsHandlerRequest request, ServerCallContext context)
        {
            return Task.FromResult(new EnumsHandlerReply
            {
                Gender = request.Gender == genders.Female ? genders.Male : genders.Female
            });
        }
    }
}
