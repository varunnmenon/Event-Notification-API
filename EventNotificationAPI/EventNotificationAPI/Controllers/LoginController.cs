using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventNotificationAPI.Models;
using EventNotificationAPI.BusinessLogic;

namespace EventNotificationAPI.Controllers
{
    public class LoginController : ApiController
    {
        //Create Login Instance IDs
        [ActionName("CreateLoginDetails")]
        [HttpPost]
        public HttpResponseMessage CreateLoginDetails(LoginRequest login)
        {
            LoginDetails loginDetails = new LoginDetails();
            try
            {
                bool result = loginDetails.CreateLogin(login);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        //Update Login Instance Ids
        [ActionName("ModifyLoginDetails")]
        [HttpPost]
        public HttpResponseMessage ModifyLoginDetails(LoginRequest login)
        {
            LoginDetails loginDetails = new LoginDetails();
            try
            {
                bool result = loginDetails.ModifyLogin(login);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

    }
}
