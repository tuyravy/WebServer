using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Monthly_Reporting.Models;
using Monthly_Reporting.Properties;

namespace Monthly_Reporting.Controllers
{
    public class PaidOffController : Controller
    {
        SqlConnection sqlcon = new SqlConnection();
        Utility mutility = new Utility();
        PaidOff paid_off = new PaidOff();
        // GET: PaidOff
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtpaidoff = new DataTable();
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
                    string concat = "brcode>500 ";
                    dtpaidoff = paid_off.paidoff(concat);
                    ViewBag.paidoff = dtpaidoff;
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }

            }
        }
        [HttpPost]
        public ActionResult SearchReportdatePaidOff(PaidOff piad_off)
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtpaidoff = new DataTable();
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
                    string concat = "brcode>500 ";
                    dtpaidoff = paid_off.paidoff(concat);
                    ViewBag.paidoff = dtpaidoff;
                    ViewBag.date_start = paid_off.date_start;
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }

            }
        }



        [HttpPost]
        public ActionResult SearchReportdatePaidOffSummary(PaidOff piad_off)
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtpaidoff = new DataTable();

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
                    string concat = "brcode>500 ";
                    dtpaidoff = paid_off.paidoff(concat);
                    ViewBag.paidoff = dtpaidoff;
                    ViewBag.date_start = paid_off.date_start;
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }

            }
        }


        public ActionResult PaidOffAll()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtpaidoff = new DataTable();
            UserAccessRightController UsAccRight = new UserAccessRightController();
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
                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
            ViewBag.sub_manin = dt;
            string concat = "brcode between 100  and 600";
            dtpaidoff = paid_off.paidoff(concat);
            ViewBag.paidoff = dtpaidoff;
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
    
