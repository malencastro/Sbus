using Grpc.Core;
using Grpc.Net.Client;
using GrpcGreeterClient;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7222");


// Average
var averageClient = new AverageCalculator.AverageCalculatorClient(channel);
var request = new AverageRequest();
var stream = averageClient.CalculateAverage();
foreach (var i in Enumerable.Range(1, 10))
{
    request.InputNumber = i;
    await stream.RequestStream.WriteAsync(request);
}
// I need to tell the server that I'm done writing on the stream
await stream.RequestStream.CompleteAsync();
var response = await stream.ResponseAsync;
Console.WriteLine($"The average is: {response.Average}");





// Greeter
var greeterClient = new Greeter.GreeterClient(channel);
var reply = await greeterClient.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("Greeting: " + reply.Message);




// Sum
var sumClient = new Sum.SumClient(channel);
var sumRequest = new SumRequest
{
    NumberOne = 5,
    NumberTwo = 3
};
var sumReply = sumClient.AddTwoNumbers(sumRequest);
Console.WriteLine($"Sum of {sumRequest.NumberOne} and {sumRequest.NumberTwo} is {sumReply.SumResult}");




// Prime numbers
var primeClient = new DecomposeNumberService.DecomposeNumberServiceClient(channel);
var primeRequest = new DecomposeNumberRequest
{
    InitialNumber = 120
};
var primeReply = primeClient.DecomposeNumber(new DecomposeNumberRequest { InitialNumber = 120 });
var primeResult = $"Number {primeRequest.InitialNumber} can be decomposed into: ";
while (await primeReply.ResponseStream.MoveNext())
{
    primeResult += primeReply.ResponseStream.Current.DecomposedNumber + ", ";
}
Console.WriteLine(primeResult);




Console.WriteLine("Press any key to exit...");
Console.ReadKey();