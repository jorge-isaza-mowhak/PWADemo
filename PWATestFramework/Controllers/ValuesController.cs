using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PWADemo.Models;
using System.Web.Script.Serialization;
using System.Web.Http.Cors;
using System.Threading;

namespace PWATestFramework.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [EnableCors(origins: "http://localhost:49330", headers: "*", methods: "*")]
        public OcularResponse Get(String driverName)
        {
            if(driverName == null)
            {
                driverName = "";
            }
            OcularResponse res = new OcularResponse{ 
                Accepted = "no",
                Name = driverName
            };

            if(driverName.ToLower() != "")
            {
                res.Accepted = "yes";
            }

            Thread.Sleep(5000);

            var json = new JavaScriptSerializer().Serialize(res);
            return res;
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
