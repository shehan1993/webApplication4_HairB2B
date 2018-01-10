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
    public class galary{

        
        public string [] photos;

    }

    public class getGalleryController : ApiController
    {
        List<object> gallery = new List<object>();
        public List<object> get(int id)
        {

            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Gallery WHERE profile_id = '" + id + "'", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    galary p = new galary();

                   
                    
                    gallery.Add(p);
                }
            }


            return gallery;
        }

    }
}
