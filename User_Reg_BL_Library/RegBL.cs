using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchFlightModelLibrary;
using Airline_Reg_DAL_Library;


namespace User_Reg_BL_Library
{
    public class RegBL
    {
        RegDAL dal;
        public RegBL()
        {
            dal = new RegDAL();
        }
        public bool Login(string email, string password)
        {
            bool loginStatus = false;
            string databasePassword = dal.FetchPassword(email);
            if (databasePassword == password)
                loginStatus = true;
            return loginStatus;
        }


        public bool Insert_userBl(string ufname, string ulname, string dob, string nat, string pnum, string gender, string gmail, string password)
        {
            return dal.Insert_user(ufname, ulname, dob, nat, pnum, gender, gmail, password);
        }
        public List<SearchFlight> getDetails(string fldate, string source, string des)
        {
            return dal.GetFlightDetails(fldate, source, des);
        }
        public List<string> FetchPlaceNames()
        {
            return dal.FetchCityName();
        }
    }
}
