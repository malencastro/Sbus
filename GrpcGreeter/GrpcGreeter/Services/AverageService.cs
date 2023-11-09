using Grpc.Core;

namespace GrpcGreeter.Services
{
    public class AverageService : AverageCalculator.AverageCalculatorBase
    {
        private readonly ILogger<AverageService> _logger;
        public AverageService(ILogger<AverageService> logger)
        {
            _logger = logger;
        }

        public override async Task<AverageResponse> CalculateAverage(IAsyncStreamReader<AverageRequest> requestStream, ServerCallContext context)
        {
            var sum = 0;
            var count = 0.0;
            while (await requestStream.MoveNext())
            {
                sum += requestStream.Current.InputNumber;
                count++;
            }

            var average = sum / count;
            return new AverageResponse
            {
                Average = average
            };
        }
    }
}
