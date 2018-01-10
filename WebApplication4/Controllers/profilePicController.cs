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
    public class images
    {
        public Int32 id;
        public string image;
    }

    /**Profile Pictures */
    public class profilePicController : ApiController
    {
   public List<object> Get()
        {
            List<object> images = new List<object>();
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM proImages", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    images img = new images();
                    img.id = Convert.ToInt32(rdr["id"]);
                    img.image = rdr["proPic"].ToString();
                    images.Add(img);
                }
            }

            return images;
        }

    }
}
