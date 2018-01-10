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
    public class stylists {
        public Int32 id;
        public string first_name;
        public string last_name;
        public string address_line_01;
        public string address_line_02;
        public string city;
        public string state;
        public string zip;
        public string country;
        public string Telephone;
        public string description;
        public string term_and_conditions;
        public string charge_per_slot;
        public string src;
       

    }

    /**Top 4 stylist for initial loading*/
    public class getStylistsProfileDetailsController : ApiController
    {
        List<object> dataOfStylists = new List<object>();
        public List<object> GetStylistsDetails()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT  TOP 4 * FROM trn_stylist LEFT JOIN proImages ON trn_stylist.profile_id = proImages.id ", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    stylists stl = new stylists();
                    stl.id = Convert.ToInt32(rdr["profile_id"]);
                    stl.first_name = rdr["first_name"].ToString();
                    stl.last_name = rdr["last_name"].ToString();
                    stl.address_line_01 = rdr["address_line_01"].ToString();
                    stl.address_line_02 = rdr["address_line_02"].ToString();
                    stl.city = rdr["city"].ToString();
                    stl.state = rdr["state"].ToString();
                    stl.zip = rdr["zip"].ToString();
                    stl.country = rdr["country"].ToString();
                    stl.Telephone = rdr["Telephone"].ToString();
                    stl.description = rdr["description"].ToString();
                    stl.term_and_conditions = rdr["term_and_conditions"].ToString();
                    stl.charge_per_slot = rdr["charge_per_slot"].ToString();
                    stl.src = rdr["proPic"].ToString();

                    dataOfStylists.Add(stl);

                }
            }

            return dataOfStylists;
        }


    }
}
