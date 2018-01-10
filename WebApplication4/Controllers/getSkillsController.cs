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
    public class skills
    {
       public Int32 id;
       public string skill;

    }

    public class dataForAdvanceSearch
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

    /**Skills for advance search*/
    public class getSkillsController : ApiController
    {
        List<object> skills = new List<object>();

        public List<object> get()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM trn_job_role", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    skills skl = new skills();

                    skl.id = Convert.ToInt32(rdr["id"]);
                    skl.skill = rdr["role"].ToString();
                    skills.Add(skl);
                }
            }

            return skills;

        }

        /** stylists with required skill */
        public List<object> get(int id)
        {
            List<object> advaceSearchDetails = new List<object>();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                //if id = 1000 then all styles are select
                if (id != 1000)
                {
                    SqlCommand cmd = new SqlCommand("SELECT  * FROM trn_stylist LEFT JOIN proImages ON trn_stylist.profile_id = proImages.id WHERE job_role_id ='" + id + "'", con);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        dataForAdvanceSearch ads = new dataForAdvanceSearch();

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

                        advaceSearchDetails.Add(ads);
                    }
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT  TOP 4 * FROM trn_stylist LEFT JOIN proImages ON trn_stylist.profile_id = proImages.id", con);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        dataForAdvanceSearch ads = new dataForAdvanceSearch();

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

                        advaceSearchDetails.Add(ads);
                    }
                }
                
                
            }

            return advaceSearchDetails;

        }

    }
    }
