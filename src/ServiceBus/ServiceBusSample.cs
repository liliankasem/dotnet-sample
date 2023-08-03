// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SampleApp
{
    public class ServiceBusSample
    {
        private readonly ILogger<ServiceBusSample> _logger;

        public ServiceBusSample(ILogger<ServiceBusSample> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This functions demonstrates that it is possible to bind to both the ServiceBusReceivedMessage and any of the supported binding contract
        /// properties at the same time. If attempting this, the ServiceBusReceivedMessage must be the first parameter. There is not
        /// much benefit to doing this as all of the binding contract properties are available as properties on the ServiceBusReceivedMessage.
        /// </summary>
        [Function(nameof(ServiceBusReceivedMessageFunction))]
        public void ServiceBusReceivedMessageFunction(
            [ServiceBusTrigger("queue", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message, string messageId, int deliveryCount)
        {
            // The MessageId property and the messageId parameter are the same.
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message ID: {id}", messageId);

            // Similarly the DeliveryCount property and the deliveryCount parameter are the same.
            _logger.LogInformation("Delivery Count: {count}", message.DeliveryCount);
            _logger.LogInformation("Delivery Count: {count}", deliveryCount);
        }
    }
}