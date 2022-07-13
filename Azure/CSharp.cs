using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ConfigCat.Client;

namespace My.Functions
{
    public static class CSharp
    {
        [FunctionName("CSharp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            Microsoft.Extensions.Logging.ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            var clientConfiguration = new LazyLoadConfiguration
{
  SdkKey = "f3vYCGwC00OC4a_P2E5x4w/U6mbty2Tq0aCDCws85-xvw", // <-- This is the actual SDK Key for your Production environment
  CacheTimeToLiveSeconds = 1
};
IConfigCatClient client = new ConfigCatClient(clientConfiguration);

User user = new User("returning"); // Unique identifier is required. Could be UserID, Email address or SessionID.

var costumerflag = client.GetValue("costumerflag", false, user);
var isAwesomeFeatureEnabled = client.GetValue("isAwesomeFeatureEnabled", false, user);

Console.WriteLine("costumerflag's value from ConfigCat: " + costumerflag);
Console.WriteLine("isAwesomeFeatureEnabled's value from ConfigCat: " + isAwesomeFeatureEnabled);

            string responseMessage = costumerflag
                ? $"Welcome back, {name}!"
                : $"Welcome {name}!";

            return new OkObjectResult(responseMessage);
        }
    }
}