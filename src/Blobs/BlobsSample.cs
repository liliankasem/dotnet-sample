// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace WorkerBindingSamples
{
    public class BlobsSample
    {
        private readonly ILogger<BlobsSample> _logger;

        public BlobsSample(ILogger<BlobsSample> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This sample demonstrates how to retrieve the contents of a blob file when a blob
        /// is added or updated in the given container. The code uses a <see cref="BlobClient"/>
        /// instance to read contents of the blob. The string {name} in the blob trigger path
        /// creates a binding expression that you can use in function code to access the file
        /// name of the triggering blob.
        /// The <see cref="BlobClient"/> instance could also be used for write operations.
        /// </summary>
        [Function(nameof(BlobDownloadFunction))]
        public async Task BlobDownloadFunction(
            [BlobTrigger("upload/{name}")] BlobClient client, string name,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Downloading blob '{name}' ...", name);

            try
            {
                using (Stream fs = File.Create(name))
                await client.DownloadToAsync(fs, cancellationToken);
                _logger.LogInformation("Download complete.");
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Download canceled. Deleting {0}...", name);
                File.Delete(name);
            }
        }

        /// <summary>
        /// This sample demonstrates how to retrieve the contents of a given blob file.
        /// The code uses a <see cref="BlobClient"/> instance to read contents of the blob.
        /// The <see cref="BlobClient"/> instance could also be used for write operations.
        /// </summary>
        [Function(nameof(BlobClientInputFunction))]
        public async Task<IActionResult> BlobClientInputFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req,
            [BlobInput("input-container/sample1.txt")] BlobClient client)
        {
            var downloadResult = await client.DownloadContentAsync();
            var content = downloadResult.Value.Content.ToString();

            _logger.LogInformation("Blob content: {content}", content);

            return new OkObjectResult(content);
        }
    }
}