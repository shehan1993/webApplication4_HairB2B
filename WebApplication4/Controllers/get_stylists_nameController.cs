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
    public class datas
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
        public string job_role_id;
        public string src;
    }

    /**Names of the stylist for search menu*/
    public class get_stylists_nameController : ApiController
    {
         // GET api/get_stylists_name
        public List<string> GetAllStylistsName()
        {
            List<string> names = new List<string>();
            List<string> ids = new List<string>();

            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            string[] x = new string[7];
            
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT name,id FROM trn_User_Account", con);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    Debug.WriteLine(rdr["name"]);
                    names.Add(rdr["name"].ToString());
                    names.Add(rdr["id"].ToString());
                   

                                     
                }
            }

            

            return names;
          
        }


        /** All details of required stylist for search result*/
        // GET api/get_stylists_name
        public List<object> GetOneStylistDetails(int id)
        {
            List<object> details = new List<object>();
            int job_role_id=0;

            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            string[] x = new string[7];

            datas d = new datas();
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM trn_stylist LEFT JOIN proImages ON trn_stylist.profile_id = proImages.id WHERE trn_stylist.profile_id = '" + id +"'", con);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    
                    d.id = Convert.ToInt32(rdr["profile_id"]);
                    d.first_name = rdr["first_name"].ToString();
                    d.last_name = rdr["last_name"].ToString();
                    d.address_line_01 = rdr["address_line_01"].ToString();
                    d.address_line_02 = rdr["address_line_02"].ToString();
                    d.city = rdr["city"].ToString();
                    d.state = rdr["state"].ToString();
                    d.zip = rdr["zip"].ToString();
                    d.country  = rdr["country"].ToString();
                    d.Telephone = rdr["Telephone"].ToString();
                    d.description = rdr["description"].ToString();
                    d.term_and_conditions = rdr["term_and_conditions"].ToString();
                    d.charge_per_slot = rdr["charge_per_slot"].ToString();
                    job_role_id = Convert.ToInt32(rdr["job_role_id"]);
                    d.src = rdr["proPic"].ToString();


                }


            }


            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT role FROM trn_job_role WHERE id = '" + job_role_id+"'", con);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {


                    d.job_role_id =rdr["role"].ToString();

                }
            }

            details.Add(d);
            return details;

        }




    }
}
