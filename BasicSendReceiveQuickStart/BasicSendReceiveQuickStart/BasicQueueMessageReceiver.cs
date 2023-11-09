using Azure.Messaging.ServiceBus;

namespace BasicSendReceiveQuickStart
{
    public class BasicQueueMessageReceiver
    {
        public ServiceBusClient Connect()
        {
            var connectionString = "Endpoint=sb://containerappjobspoc.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=+F0zXsuM/fi9oxRs1sfEsbHNaqmdB68kx+ASbGonLik=";

            var client = new ServiceBusClient(connectionString);
            return client;
        }

        public async Task Receive()
        {
            ServiceBusClient client = Connect();
            ServiceBusReceiver receiver = client.CreateReceiver("basic-queue");

            ServiceBusReceivedMessage message = await receiver.ReceiveMessageAsync(TimeSpan.FromSeconds(5));

            if (message != null)
            {
                Console.WriteLine("Received Single Message: " + message.Body);
                await receiver.CompleteMessageAsync(message);
            }
            else
            {
                Console.WriteLine("Didnt receive a message");
            }
        }
    }
}