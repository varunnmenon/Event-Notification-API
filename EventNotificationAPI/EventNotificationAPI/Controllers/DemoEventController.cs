using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventNotificationAPI.RestfulLayer;

namespace EventNotificationAPI.Controllers
{
    public class DemoEventController : ApiController
    {
        //public string Get()
        //{
        //    return "Welcome To Web API";
        //}
        //public List<string> Get(int Id)
        //{
        //    return new List<string> {
        //        "Data1",
        //        "Data2"
        //    };
        //}

        [ActionName("GetDataList")]
        [HttpGet]
        public HttpResponseMessage GetDataList(int Id = 0)
        {
            List <string> list1 = new List<string>
            {
                "Data1",
                "Data2"
            };
            return Request.CreateResponse(HttpStatusCode.OK, list1);
        }

        [ActionName("GetZipcodes")]
        [HttpGet]
        public HttpResponseMessage GetZipcodes()
        {
           List<Int32> ListNumber = new List<Int32>
            {
                400078
            };
            return Request.CreateResponse(HttpStatusCode.OK, "400078");
        }

        [ActionName("TestFireBaseCalls")]
        [HttpGet]
        public HttpResponseMessage TestFireBaseCalls()
        {
            int result = 0;
            result = new MakeFireBaseCalls().SendPushNotification();
            return Request.CreateResponse(HttpStatusCode.OK, new List<int> { result });
        }
    }
}
