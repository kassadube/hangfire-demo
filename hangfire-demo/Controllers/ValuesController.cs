using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Hangfire;

namespace hangfire_demo.Controllers
{
    [Produces("application/json")]
    [Route("api/Values")]
    public class ValuesController : Controller
    {
        public void RunInBackground()
        {
            Console.WriteLine($"Running in the background: {Thread.CurrentThread.Name}");
        }

        public void ReRunInBackground()
        {
            RecurringJob.AddOrUpdate(
                () => RunInBackground(),
                Cron.MinuteInterval(1));
        }
        // GET: api/Values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            ReRunInBackground();
            BackgroundJob.Enqueue(() => RunInBackground());
            Console.WriteLine($"Running in the background: {Thread.CurrentThread.Name}");
            return new string[] { "value1", "value2" };
        }

        // GET: api/Values/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
