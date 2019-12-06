using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserLoginModelLibrary;
using User_Reg_BL_Library;
using System.Web.Http.Cors;

namespace FlightBookingWepApiProject.Controllers
{
    [EnableCors("http://localhost:4200", "*", "GET,PUT,POST")]
    public class LoginController : ApiController
    {
        // GET: api/Login
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        RegBL bl = new RegBL();

        static List<UserLogin> users = new List<UserLogin>();
        public Boolean Get(string email, string upass)
        {

            if (bl.Login(email, upass))
            {

                return true;
            }
            else
            {
                return false;
            }

        }
        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Login
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
