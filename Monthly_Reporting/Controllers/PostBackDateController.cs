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
    public class PostBackDateController : Controller
    {
        SqlConnection sqlcon = new SqlConnection();
        Utility mutility = new Utility();
        PostBackDate pd = new PostBackDate();
        // GET: PostBackDate
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtbackdate = new DataTable();
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
                    string sql = "select * from Rep_PostBackDate";
                    dtbackdate = pd.PostBack_Date(sql);
                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    ViewBag.backdate = dtbackdate;
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