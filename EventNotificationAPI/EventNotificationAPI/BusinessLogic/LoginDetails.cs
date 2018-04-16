using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventNotificationAPI.Models;
using EventNotificationAPI.DataAccessLayer;

namespace EventNotificationAPI.BusinessLogic
{
    public class LoginDetails
    {

        public bool CreateLogin(LoginRequest login)
        {
            try
            {
                Login loginDAL = new Login();
                bool success = loginDAL.CreateLogin(login);
                if (success)
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public bool ModifyLogin(LoginRequest login)
        {
            try
            {
                Login loginDAL = new Login();
                bool success = loginDAL.ModifyLogin(login);
                if (success)
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
    }
}