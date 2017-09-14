using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ServiceModel.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AngularTestService
{
    public class Authontication
    {
        [WebInvoke(Method = "GET",
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "data/{id}")]
        public DataSet Authonticate(string uname, string upwd)
        {
            DataSet ds = new DataSet();
            try
            {
                
                string strcon = ConfigurationManager.ConnectionStrings["ConnectionLocaldb"].ToString();
                SqlConnection conn = new SqlConnection(strcon);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "usp_getUserdetails";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = uname;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = upwd;
               
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(ds);
                conn.Close();


               
            }
            catch(Exception ex)
            {
                
            }

            return ds;
        }


        public class Userdetails
        {
            public string uname { get; set; }
            public string upwd { get; set; }
        }

    }
}