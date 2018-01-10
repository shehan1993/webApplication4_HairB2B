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
    public class paymentFilter
    {
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

    public class getPaymentsController : ApiController
    {
        List<object> priceFilter = new List<object>();

        public List<object> get(int id)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT  * FROM trn_stylist LEFT JOIN proImages ON trn_stylist.profile_id = proImages.id WHERE charge_per_slot <='" + id + "'", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    paymentFilter ads = new paymentFilter();

                    ads.id = Convert.ToInt32(rdr["profile_id"]);
                    ads.first_name = rdr["first_name"].ToString();
                    ads.last_name = rdr["last_name"].ToString();
                    ads.address_line_01 = rdr["address_line_01"].ToString();
                    ads.address_line_02 = rdr["address_line_02"].ToString();
                    ads.city = rdr["city"].ToString();
                    ads.state = rdr["state"].ToString();
                    ads.zip = rdr["zip"].ToString();
                    ads.country = rdr["country"].ToString();
                    ads.Telephone = rdr["Telephone"].ToString();
                    ads.description = rdr["description"].ToString();
                    ads.term_and_conditions = rdr["term_and_conditions"].ToString();
                    ads.charge_per_slot = rdr["charge_per_slot"].ToString();

                    ads.src = rdr["proPic"].ToString();

                    priceFilter.Add(ads);
                }
            }

            return priceFilter;

        }
    }
}
