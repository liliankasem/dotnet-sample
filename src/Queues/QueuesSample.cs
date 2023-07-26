// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SampleApp
{
    public class QueueSamples
    {
        private readonly ILogger<QueueSamples> _logger;

        public QueueSamples(ILogger<QueueSamples> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This function demonstrates binding to a single <see cref="QueueMessage"/>.
        /// </summary>
        [Function(nameof(QueueMessageFunction))]
        public void QueueMessageFunction(
            [QueueTrigger("input-queue")] QueueMessage message)
        {
            _logger.LogInformation(message.MessageText);
        }
    }
}