using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Monthly_Reporting.Models;
using Monthly_Reporting.Properties;
using System.Configuration;
namespace Monthly_Reporting.Controllers
{
    public class HomeController : Controller
    {  
        SqlConnection sqlcon = new SqlConnection();
        Utility mutility = new Utility();
        ReportDateController CurrRunDate = new ReportDateController();
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
           

            mutility.DbName = Settings.Default["DbName"].ToString();
            mutility.UserName = Settings.Default["UserName"].ToString();
            mutility.Password = Settings.Default["Password"].ToString();
            mutility.ServerName= Settings.Default["ServerName"].ToString();

            if (mutility.ServerName=="" || mutility.DbName=="" || mutility.UserName =="")
            {
                return RedirectToAction("Index", "Setting_Connection/Index");
            }
            else
            {
                if (Session["ID"] != null)
                {
                    UserAccessRightController UsAccRight = new UserAccessRightController();
                    string userkey =Convert.ToString(Session["user_key"]);
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    ViewBag.CurrRunDate = CurrRunDate.CurrRunDate(userkey);
                    return View();

                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }

                
            }

            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
           
            return View();
        }
       
    }
}