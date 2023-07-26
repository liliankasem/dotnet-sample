// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace WorkerBindingSamples
{
    public class CosmosSample
    {
        private readonly ILogger<CosmosSample> _logger;

        public CosmosSample(ILogger<CosmosSample> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// This sample demonstrates how to retrieve a collection of documents.
        /// The code uses a <see cref="CosmosClient"/> instance to read a list of documents.
        /// The <see cref="CosmosClient"/> instance could also be used for write operations.
        /// </summary>
        [Function(nameof(DocsByUsingCosmosClient))]
        public async Task<IActionResult>  DocsByUsingCosmosClient(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req,
            [CosmosDBInput(Connection = "CosmosDBConnection")] CosmosClient client)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var iterator = client.GetContainer("ToDoItems", "Items")
                                 .GetItemQueryIterator<ToDoItem>("SELECT * FROM c");

            List<string> todoItems = new();

            while (iterator.HasMoreResults)
            {
                var documents = await iterator.ReadNextAsync();
                foreach (ToDoItem item in documents)
                {
                    todoItems.Add(item.Description);
                }
            }

            return new OkObjectResult(string.Join(", ", todoItems.ToArray()));
        }

        /// <summary>
        /// This sample demonstrates how to retrieve a collection of documents.
        /// The function is triggered by an HTTP request. The query is specified in the <see cref="CosmosDBInputAttribute.SqlQuery"/> attribute property.
        /// </summary>
        [Function(nameof(DocsBySqlQuery))]
        public IActionResult DocsBySqlQuery(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req,
            [CosmosDBInput(
                databaseName: "ToDoItems",
                containerName: "Items",
                Connection = "CosmosDBConnection",
                SqlQuery = "SELECT * FROM ToDoItems t WHERE CONTAINS(t.description, 'cat')")] IEnumerable<ToDoItem> toDoItems)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            List<string> todoItems = new();

            foreach (ToDoItem item in toDoItems)
            {
                todoItems.Add(item.Description);
            }

            return new OkObjectResult(string.Join(", ", todoItems.ToArray()));
        }

        public class ToDoItem
        {
            public string Id { get; set; }
            public string Description { get; set; }
        }
    }
}