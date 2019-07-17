using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Monthly_Reporting.Models;
using System.Data;
using System.Data.SqlClient;
using Monthly_Reporting.Properties;
using System.Configuration;
namespace Monthly_Reporting.Controllers
{
    public class Top10CoIncentiveController : Controller
    {
        SqlConnection sqlcon = new SqlConnection();
        Utility mutility = new Utility();
        // GET: Top10CoIncentive
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();


            mutility.DbName = Settings.Default["DbName"].ToString();
            mutility.UserName = Settings.Default["UserName"].ToString();
            mutility.Password = Settings.Default["Password"].ToString();
            mutility.ServerName = Settings.Default["ServerName"].ToString();

            if (mutility.ServerName == "" || mutility.DbName == "" || mutility.UserName == "")
            {
                return RedirectToAction("Index", "Setting_Connection/Index");
            }
            else
            {
                if (Session["ID"] != null)
                {
                    UserAccessRightController UsAccRight = new UserAccessRightController();

                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }


            }

        }
        public ActionResult Branch_perfix()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();


            mutility.DbName = Settings.Default["DbName"].ToString();
            mutility.UserName = Settings.Default["UserName"].ToString();
            mutility.Password = Settings.Default["Password"].ToString();
            mutility.ServerName = Settings.Default["ServerName"].ToString();

            if (mutility.ServerName == "" || mutility.DbName == "" || mutility.UserName == "")
            {
                return RedirectToAction("Index", "Setting_Connection/Index");
            }
            else
            {
                if (Session["ID"] != null)
                {
                    UserAccessRightController UsAccRight = new UserAccessRightController();

                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }


            }
        }
        public ActionResult Top1in3month()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();


            mutility.DbName = Settings.Default["DbName"].ToString();
            mutility.UserName = Settings.Default["UserName"].ToString();
            mutility.Password = Settings.Default["Password"].ToString();
            mutility.ServerName = Settings.Default["ServerName"].ToString();

            if (mutility.ServerName == "" || mutility.DbName == "" || mutility.UserName == "")
            {
                return RedirectToAction("Index", "Setting_Connection/Index");
            }
            else
            {
                if (Session["ID"] != null)
                {
                    UserAccessRightController UsAccRight = new UserAccessRightController();

                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }


            }
        }
    }
}