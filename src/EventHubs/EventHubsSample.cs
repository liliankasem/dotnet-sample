// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace WorkerBindingSamples
{
    public class EventHubsSample
    {
        private readonly ILogger<EventHubsSample> _logger;

        public EventHubsSample(ILogger<EventHubsSample> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This function demonstrates binding to an array of <see cref="EventData"/>.
        /// </summary>
        [Function(nameof(EventDataBatchFunction))]
        public void EventDataBatchFunction(
            [EventHubTrigger("queue", Connection = "EventHubConnection")] EventData[] events)
        {
            foreach (EventData @event in events)
            {
                _logger.LogInformation("Event Body: {body}", @event.Body);
                _logger.LogInformation("Event Content-Type: {contentType}", @event.ContentType);
            }
        }
    }
}