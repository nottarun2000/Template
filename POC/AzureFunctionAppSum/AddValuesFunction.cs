using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctionAppSum
{
    public static class AddValuesFunction
    {
        [FunctionName("AddValues")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
        {
            // Read input data from the request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            if (data != null && data.value1 != null && data.value2 != null)
            {
                // Perform the addition
                int value1 = data.value1;
                int value2 = data.value2;
                int result = value1 + value2;

                return new OkObjectResult(new { result });
            }
            else
            {
                return new BadRequestObjectResult("Invalid input data");
            }
        }
    }
}