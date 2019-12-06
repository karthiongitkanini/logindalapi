using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SearchFlightModelLibrary;

namespace Airline_Reg_DAL_Library
{
    public class RegDAL
    {
        SqlConnection conn;
        SqlCommand  cmdFetchFlightDetails, cmdFetchCityName, cmdFetchPassword, cmdInsertUser;
        public RegDAL()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conFlight"].ConnectionString);
        }

        public string FetchPassword(string email)
        {
            cmdFetchPassword = new SqlCommand("Proc_UserLogin", conn);
            cmdFetchPassword.Parameters.Add("@email", SqlDbType.VarChar, 20);
            cmdFetchPassword.Parameters.Add("@password", SqlDbType.VarChar, 20);
            cmdFetchPassword.CommandType = CommandType.StoredProcedure;
            string password = null;

            conn.Open();

            cmdFetchPassword.Parameters[0].Value = email;
            cmdFetchPassword.Parameters[1].Direction = ParameterDirection.Output;
            cmdFetchPassword.ExecuteNonQuery();
            password = cmdFetchPassword.Parameters[1].Value.ToString();
            conn.Close();
            return password;

        }
        public bool CheckUser(string username)
        {
            bool val = false;
            cmdFetchPassword = new SqlCommand("proc_login", conn);
            cmdFetchPassword.Parameters.Add("@un", SqlDbType.VarChar, 20);
            cmdFetchPassword.Parameters.Add("@pass", SqlDbType.VarChar, 20);
            cmdFetchPassword.CommandType = CommandType.StoredProcedure;
            string password = null;

            conn.Open();

            cmdFetchPassword.Parameters[0].Value = username;
            cmdFetchPassword.Parameters[1].Direction = ParameterDirection.Output;
            cmdFetchPassword.ExecuteNonQuery();
            password = cmdFetchPassword.Parameters[1].Value.ToString();
            if (password != null)
            {
                val = true;
            }
            conn.Close();
            return val;

        }
        public List<SearchFlight> GetFlightDetails(string fldate, string source, string destination)
        {
            List<SearchFlight> details = new List<SearchFlight>();
            conn.Open();
            cmdFetchFlightDetails = new SqlCommand("proc_SearchFlight", conn);
            cmdFetchFlightDetails.Parameters.Add("@date", SqlDbType.VarChar, 10);
            cmdFetchFlightDetails.Parameters.Add("@source", SqlDbType.VarChar, 20);
            cmdFetchFlightDetails.Parameters.Add("@destination", SqlDbType.VarChar, 20);
            cmdFetchFlightDetails.CommandType = CommandType.StoredProcedure;
            cmdFetchFlightDetails.Parameters[0].Value = fldate;
            cmdFetchFlightDetails.Parameters[1].Value = source;
            cmdFetchFlightDetails.Parameters[2].Value = destination;
            SqlDataReader drFlightDetails = cmdFetchFlightDetails.ExecuteReader();
            SearchFlight search = null;
            //if (drFlightDetails.HasRows == false)
            //    throw new NoFlightInDatabaseException();
            while (drFlightDetails.Read())
            {
                search = new SearchFlight();
                search.Flightid = drFlightDetails[0].ToString();
                search.Departuretime = drFlightDetails[1].ToString();
                search.Arrivaltime = drFlightDetails[2].ToString();
                search.Duration = drFlightDetails[3].ToString();
                search.Fare = drFlightDetails[4].ToString();
                details.Add(search);
            }
            return details;

        }

        public List<string> FetchCityName()
        {
            List<string> cityName = new List<string>();
            cmdFetchCityName = new SqlCommand("proc_FetchCityName", conn);
            conn.Open();
            SqlDataReader drCityName = cmdFetchCityName.ExecuteReader();
            while (drCityName.Read())
            {
                cityName.Add(drCityName[0].ToString());
            }
            conn.Close();
            return cityName;
        }


        public bool Insert_user(string ufname, string ulname, string dob, string nat, string pnum, string gender, string gmail, string password)
        {
            bool return_value = false;
            cmdInsertUser = new SqlCommand("Insert_User_data", conn);
            cmdInsertUser.Parameters.Add("@ufname", SqlDbType.VarChar, 20);
            cmdInsertUser.Parameters.Add("@ulname", SqlDbType.VarChar, 20);
            cmdInsertUser.Parameters.Add("@dob", SqlDbType.Date);
            cmdInsertUser.Parameters.Add("@nat", SqlDbType.VarChar, 20);
            cmdInsertUser.Parameters.Add("@pnumber", SqlDbType.VarChar, 20);
            cmdInsertUser.Parameters.Add("@gender", SqlDbType.VarChar, 20);
            cmdInsertUser.Parameters.Add("@gmail", SqlDbType.VarChar, 20);
            cmdInsertUser.Parameters.Add("@password", SqlDbType.VarChar, 20);
            cmdInsertUser.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmdInsertUser.Parameters[0].Value = ufname;
            cmdInsertUser.Parameters[1].Value = ulname;
            cmdInsertUser.Parameters[2].Value = dob;
            cmdInsertUser.Parameters[3].Value = nat;
            cmdInsertUser.Parameters[4].Value = pnum;
            cmdInsertUser.Parameters[5].Value = gender;
            cmdInsertUser.Parameters[6].Value = gmail;
            cmdInsertUser.Parameters[7].Value = password;
            //try
            //{
            if (cmdInsertUser.ExecuteNonQuery() > 0)
            {
                return_value = true;
            }
            // }
            //  catch(SqlException e)
            // {
            //     Console.WriteLine(e.Message);
            //  }
            conn.Close();
            return return_value;



        }

    }

}
