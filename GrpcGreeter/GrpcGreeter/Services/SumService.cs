using Grpc.Core;

namespace GrpcGreeter.Services
{
    public class SumService : Sum.SumBase
    {
        private readonly ILogger<SumService> _logger;

        public SumService(ILogger<SumService> logger)
        {
            _logger = logger;
        }

        public override Task<SumResponse> AddTwoNumbers(SumRequest request, ServerCallContext context)
        {
            return Task.FromResult(new SumResponse
            {
                SumResult = request.NumberOne + request.NumberTwo
            });
        }
    }
}
