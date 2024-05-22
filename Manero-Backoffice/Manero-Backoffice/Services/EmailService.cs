using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Manero_Backoffice.Business.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Manero_Backoffice.Services
{
    public class EmailService
    {
        private readonly string serviceBusConnectionString;
        private readonly string queueName;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            serviceBusConnectionString = configuration.GetConnectionString("ServiceBus")!;
            queueName = configuration["ServiceBus:QueueName"]!;
            _logger = logger;
        }

        public async Task<bool> SendMessageAsync(EmailRequest request)
        {
            try
            {
                var jsonMessage = JsonConvert.SerializeObject(request);
                await using var client = new ServiceBusClient(serviceBusConnectionString);
                ServiceBusSender sender = client.CreateSender(queueName);
                ServiceBusMessage busMessage = new ServiceBusMessage(jsonMessage);
                await sender.SendMessageAsync(busMessage);

                return true;
            }

            catch (Exception ex)
            {
                _logger.LogError($"ERROR : EmailService.SendMessageAsync() :: {ex.Message}");
            }

            return false;
        }
    }
}
