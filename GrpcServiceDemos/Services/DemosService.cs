using Google.Protobuf;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServiceDemos
{
    public class DemosService
        : Demos.DemosBase
    {
        private readonly ILogger<DemosService> logger;
        public DemosService(ILogger<DemosService> logger)
        {
            this.logger = logger;
        }

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

        public override async Task StreamGetter(StreamGetterRequest request, IServerStreamWriter<StreamGetterReply> responseStream, ServerCallContext context)
        {
            try
            {
                using (FileStream fs = new FileStream(request.FileName, FileMode.Open))
                {
                    int bufferSize = 1024 * 1024;
                    byte[] buffer = new byte[bufferSize];
                    long forwardBytes = fs.Length - fs.Position;

                    while (forwardBytes > 0)
                    {
                        if (forwardBytes < bufferSize)
                        {
                            buffer = new byte[forwardBytes];
                        }

                        await fs.ReadAsync(buffer, 0, buffer.Length);

                        await responseStream.WriteAsync(new StreamGetterReply
                        {
                            FileBytes = UnsafeByteOperations.UnsafeWrap(buffer)
                        });

                        forwardBytes = fs.Length - fs.Position;
                    }
                }
            }
            catch (InvalidOperationException e)
            {
                logger.LogWarning(e.Message);
            }
            catch (IOException e)
            {
                logger.LogWarning(e.Message);
            }
        }
    }
}
