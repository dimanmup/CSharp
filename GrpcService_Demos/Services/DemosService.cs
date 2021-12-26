using Grpc.Core;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService_Demos.Services
{
    public class DemosService : Demos.DemosBase
    {
        public override Task<FieldsWithRulesReply> FieldsRulesHandler(FieldsWithRulesRequest request, ServerCallContext context)
        {
            return Task.FromResult(new FieldsWithRulesReply
            {
                Sum = 1
                //Sum = request.Req + request.Rep.Sum() + request.Opt,
                //RepCount = request.Rep.Count,
                //HasOpt = request.HasOpt
            });
        }
    }
}
