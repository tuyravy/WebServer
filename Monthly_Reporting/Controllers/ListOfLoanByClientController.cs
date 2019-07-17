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
    public class ListOfLoanByClientController : Controller
    {

        readonly SqlConnection sqlcon = new SqlConnection();
        Utility mutility = new Utility();
        ClientFrequency CF = new ClientFrequency();
        // GET: ListOfLoanByClient
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtclientfre = new DataTable();
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
                    string sql = "select * from Rep_ClientFrequency";                   
                    dtclientfre = CF.Client_Freq(sql);
                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    ViewBag.clientfre = dtclientfre;
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }
            }
        }
        [HttpPost]
        public ActionResult SearchReportDateClient(ClientFrequency CF)
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtclientfre = new DataTable();
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
                    string sql = "select * from Rep_ClientFrequency";
                    dtclientfre = CF.Client_Freq(sql);
                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    ViewBag.clientfre = dtclientfre;
                    ViewBag.date_start = CF.date_start;
                    return View("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }
            }
        }

        [HttpPost]
        public ActionResult SearchReportDateRM(ClientFrequency CF)
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtclientfre = new DataTable();
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
                    string sql = "select * from Rep_ClientFrequency";
                    dtclientfre = CF.Client_Freq(sql);
                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    ViewBag.clientfre = dtclientfre;
                    ViewBag.date_start = CF.date_start;
                    return View("OSFreqByRM");
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }
            }
        }
        public ActionResult OSFreqByRM()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtclientfre = new DataTable();
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
                    string sql = "select * from Rep_BalanceFrequencyRM";
                    dtclientfre = CF.Client_Freq(sql);
                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    ViewBag.clientfre = dtclientfre;
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }
            }
        
    }
        [HttpPost]
        public ActionResult SearchReportDateClientBranch(ClientFrequency CF)
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtclientfre = new DataTable();
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
                    string sql = "select * from Rep_ClientFrequency";
                    dtclientfre = CF.Client_Freq(sql);
                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    ViewBag.clientfre = dtclientfre;
                    ViewBag.date_start = CF.date_start;
                    return View("ClientFreqByBranch");
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }
            }
        }
        public ActionResult ClientFreqByBranch()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtclientfre = new DataTable();
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
                    string sql = "select * from Rep_ClientFrequencyBranch";
                    dtclientfre = CF.Client_Freq(sql);
                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    ViewBag.clientfre = dtclientfre;
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }
            }

        }
        [HttpPost]
        public ActionResult SearchReportDateClientRM(ClientFrequency CF)
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtclientfre = new DataTable();
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
                    string sql = "select * from Rep_ClientFrequency";
                    dtclientfre = CF.Client_Freq(sql);
                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    ViewBag.clientfre = dtclientfre;
                    ViewBag.date_start = CF.date_start;
                    return View("ClientFreqByRM");
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }
            }
        }
        public ActionResult ClientFreqByRM()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtclientfre = new DataTable();
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
                    string sql = "select * from Rep_ClientFrequencyRM";
                    dtclientfre = CF.Client_Freq(sql);
                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    ViewBag.clientfre = dtclientfre;
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
