using Grpc.Core;

namespace GrpcGreeter.Services
{
    public class DecomposeService : DecomposeNumberService.DecomposeNumberServiceBase
    {
        private readonly ILogger<DecomposeService> _logger;

        public DecomposeService(ILogger<DecomposeService> logger)
        {
            _logger = logger;
        }

        public override async Task DecomposeNumber(DecomposeNumberRequest request, IServerStreamWriter<DecomposeNumberResponse> responseStream, ServerCallContext context)
        {
            var number = request.InitialNumber;
            var factor = 2;
            while (number > 1)
            {
                if (number % factor == 0)
                {
                    number = number / factor;
                    await responseStream.WriteAsync(new DecomposeNumberResponse
                    {
                        DecomposedNumber = factor
                    });
                }
                else
                {
                    factor++;
                }
            }
        }
    }
}
