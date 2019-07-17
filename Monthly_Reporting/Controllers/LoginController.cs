using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Monthly_Reporting.Models;
using System.Data;
using Monthly_Reporting.Properties;
using System.Configuration;
namespace Monthly_Reporting.Controllers
{
    public class LoginController : Controller
    {
        // GET: Account
        SqlConnection SqlConn = new SqlConnection();
        Utility mutility = new Utility();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        [HttpGet]
        // GET: Login

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Verify(Login acc)
        {
            mutility.DbName = Settings.Default["DbName"].ToString();
            mutility.UserName = Settings.Default["UserName"].ToString();
            mutility.Password = Settings.Default["Password"].ToString();
            mutility.ServerName = Settings.Default["ServerName"].ToString();
            SqlConn.ConnectionString = "Data source=" + mutility.ServerName + ";Initial catalog=" + mutility.DbName + ";User ID="+mutility.UserName+";Password=" + mutility.Password;
            if (SqlConn.State != ConnectionState.Open)
            {
                SqlConn.Open();
            }
            
            cmd.Connection = SqlConn;
            cmd.CommandText = "select * from Users where UserName='" + acc.Name + "' and password='" + acc.Password + "'";
            dr = cmd.ExecuteReader();
            
            if (dr.Read())
            {
                Session["ID"] = dr["ID"].ToString();
                Session["System_Name"] = dr["System_Name"].ToString();
                Session["UserName"]= dr["UserName"].ToString();
                Session["user_key"] = dr["user_key"].ToString();
                Session["report_code"] = dr["report_code"].ToString();
                Session["branch_zone"] = dr["branch_zone"].ToString(); 
                SqlConn.Close();                
                return RedirectToAction("index", "Home");
                //index is main function and Home is main Class Name on Co
            }
            else
            {
                SqlConn.Close();
                return View("Error");
            }


        }
        //this function for log out system 
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("index", "Home");
        }
    }
}