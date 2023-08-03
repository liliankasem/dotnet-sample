// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Azure.Data.Tables;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace WorkerBindingSamples.Table
{
    public class TablesSamples
    {
        private readonly ILogger<TablesSamples> _logger;

        public TablesSamples(ILogger<TablesSamples> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This function demonstrates binding to a single <see cref="TableClient"/>.
        /// </summary>
        [Function(nameof(TableClientFunction))]
        public async Task TableClientFunction(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
            [TableInput("TableName")] TableClient table)
        {
            var tableEntity = table.QueryAsync<TableEntity>();

            await foreach (TableEntity entity in tableEntity)
            {
                _logger.LogInformation("PK={pk}, RK={rk}, Text={t}", entity.PartitionKey, entity.RowKey, entity["Text"]);
            }
        }
    }
}