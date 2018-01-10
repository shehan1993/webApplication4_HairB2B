using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication4.Controllers
{

    public class pendingDates
    {
        public Int32 year;
        public Int32 month;
        public Int32 day;
        public Int32 time;

    }

    public class getPendingDatesController : ApiController
    {
        List<object> dates = new List<object>();

        public List<object> getPendingDates(int id)
        {


            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            string[] x = new string[7];
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM PendingDates WHERE stylistId = '" + id + "'", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    pendingDates pd = new pendingDates();
                   
                    pd.time = Int32.Parse(rdr["timeSlotId"].ToString());                    
                    string s = rdr["date"].ToString();
                    s = s.Replace(" 12:00:00 AM", "");
                    string[] words = s.Split('/');
                    pd.day = Int32.Parse(words[1]);
                    pd.month = Int32.Parse(words[0]);
                    pd.year = Int32.Parse(words[2]);

                    dates.Add(pd);

                }
            }

           
            return dates;
        }


    }
}
