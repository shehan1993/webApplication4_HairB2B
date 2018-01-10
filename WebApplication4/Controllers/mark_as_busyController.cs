using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.Http;
using System.Configuration;
namespace WebApplication4.Controllers
{

    public class dates {
        public Int32 year;
        public Int32 month;
        public Int32 day;
        public Int32 time;
        public Int32 type;
        
    }

    /** Busy dates of required stylist*/
    public class mark_as_busyController : ApiController
    {

        // GET api/get_stylists_name
        public List<object> GetBusyDates(int id)
        {
            List<object> detail = new List<object>();
            int job_role_id = 0;
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            string[] x = new string[7];

            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM trn_busy_date WHERE stylist_id = '" + id + "'", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                                 
                    dates d = new dates();
                    d.time = Int32.Parse(rdr["time_slot_id"].ToString());               
                    d.type = Int32.Parse(rdr["type"].ToString());
                    string s = rdr["date"].ToString();
                    s = s.Replace(" 12:00:00 AM", "");
                    string[] words = s.Split('/');
                    d.day = Int32.Parse(words[1]);
                    d.month = Int32.Parse(words[0]);
                    d.year = Int32.Parse(words[2]);

                    detail.Add(d);

                }
            }

            return detail;
        }

    }
}
