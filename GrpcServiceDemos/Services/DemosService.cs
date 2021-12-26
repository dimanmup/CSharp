using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServiceDemos
{
    public class DemosService 
        : Demos.DemosBase
    {
        private readonly ILogger<DemosService> _logger;
        public DemosService(ILogger<DemosService> logger)
        {
            _logger = logger;
        }

        public override Task<FieldsWithRulesReply> FieldsWithRules(FieldsWithRulesRequest request, ServerCallContext context)
        {
            return Task.FromResult(new FieldsWithRulesReply
            {
                Sum = request.Req + request.Opt + request.Rep.Sum()
            });
        }
    }
}
