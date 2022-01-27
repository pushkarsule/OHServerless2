using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Company.Function
{
        public class Todo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; }
    }

    public static class TodoApiInMemory
    {
        static List<Todo> items = new List<Todo>();

        //...
    }
    public class TodoCreateModel
    {
        public string TaskDescription { get; set; }
    }

    public static class CreateRating
    {
        [FunctionName("CreateRating")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            

            
    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    var input = JsonConvert.DeserializeObject<TodoCreateModel>(requestBody);

    var todo = new Todo() { TaskDescription = input.TaskDescription };
    //items.Add(todo);
    return new OkObjectResult(todo);
        }
    }
}
