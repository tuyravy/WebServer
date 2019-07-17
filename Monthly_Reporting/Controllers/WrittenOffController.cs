using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using Monthly_Reporting.Models;
using Monthly_Reporting.Properties;
using System.Configuration;
namespace Monthly_Reporting.Controllers
{
    public class WrittenOffController : Controller
    {
        SqlConnection sqlcon = new SqlConnection();
        Utility mutility = new Utility();
        WriteOff wo = new WriteOff();
        ReportDateController CurrRunDate = new ReportDateController();

        // GET: WrittenOff
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtwrittenoff = new DataTable();
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
                    string sql = "select row_number() over(order by BrCode) as id,* from dbo.Rep_WrittenOff  where reportdate='2019-04-30'  order by BrCode ASC";
                    dtwrittenoff = wo.Write_Off(sql);
                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    ViewBag.writtenoff = dtwrittenoff;
                    ViewBag.CurrRunDate = CurrRunDate.CurrRunDate(userkey);
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }
            }
        }
        /*
         * Search report by written off with date
         */
        [HttpPost]
        public ActionResult SearchReportWrittenOff(WriteOff wo)
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtwrittenoff = new DataTable();
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
                    string sql = "select row_number() over(order by BrCode) as id,* from dbo.Rep_WrittenOff  where reportdate='"+ String.Format("{0:yyyy-MM-dd}",wo.date_start)+@"'  order by BrCode ASC";
                    dtwrittenoff = wo.Write_Off(sql);
                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    ViewBag.writtenoff = dtwrittenoff;
                    ViewBag.CurrRunDate = CurrRunDate.CurrRunDate(userkey);
                    return View("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }
            }
        }
        public ActionResult Sumbymoth()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtwrittenoff = new DataTable();
            DataTable dtColumnTitle = new DataTable();
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
                    string sql = "select row_number() over(order by BrCode) as id,* from dbo.Rep_WrittenOff  where reportdate='2019-04-30'  order by BrCode ASC";
                    dtwrittenoff = wo.Write_Off(sql);
                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    dtColumnTitle = wo.CulumnTitleWO();
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    ViewBag.writtenoff = dtwrittenoff;
                    ViewBag.ColumnTitle = dtColumnTitle;
                    ViewBag.CurrRunDate = CurrRunDate.CurrRunDate(userkey);
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }                
            }
        }
        /*
         * Search report by written off with date
         */
        [HttpPost]
        public ActionResult SearchReportWrittenOffAllMonth(WriteOff wo)
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtwrittenoff = new DataTable();
            DataTable dtColumnTitle = new DataTable();
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
                    string sql = "select row_number() over(order by BrCode) as id,* from dbo.Rep_WrittenOff  where  reportdate='" + String.Format("{0:yyyy-MM-dd}", wo.date_start) + @"'  order by BrCode ASC";
                    dtwrittenoff = wo.Write_Off(sql);
                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    ViewBag.writtenoff = dtwrittenoff;
                    ViewBag.date_start = wo.date_start;
                    ViewBag.ColumnTitle = dtColumnTitle;
                    ViewBag.CurrRunDate = CurrRunDate.CurrRunDate(userkey);
                    return View("Sumbymoth");
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }
            }
        }
        [HttpPost]
        public ActionResult SearchReportWrittenOffCompare(WriteOff wo)
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtwrittenoff = new DataTable();
            DataTable dtColumnTitle = new DataTable();
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
                    string sql = "select row_number() over(order by BrCode) as id,* from Rep_WrittenOffMBWIN  where  reportdate='" + String.Format("{0:yyyy-MM-dd}", wo.date_start) + @"'  order by BrCode ASC";
                    dtwrittenoff = wo.Write_Off(sql);
                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    ViewBag.writtenoff = dtwrittenoff;
                    ViewBag.date_start = wo.date_start;
                    ViewBag.ColumnTitle = dtColumnTitle;
                    ViewBag.CurrRunDate = CurrRunDate.CurrRunDate(userkey);
                    return View("GlComTool");
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }
            }
        }
        public ActionResult GlComTool()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtwrittenoff = new DataTable();

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
                    string sql = "select  row_number() over(order by BrCode) as id, * from dbo.Rep_WrittenOffMBWIN  order by BrCode ASC";
                    dtwrittenoff = wo.Write_Off(sql);
                    string userkey = Session["user_key"].ToString();
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    ViewBag.writtenoff = dtwrittenoff;
                    ViewBag.CurrRunDate = CurrRunDate.CurrRunDate(userkey);
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }


            }
        }
        public DataTable brlist(string userkey)
        {
            DataTable dt = new DataTable();
            string branchZone = Convert.ToString(mutility.dbSingleResult("select branch_zone from users where user_key='"+ userkey + "'"));
            var zone_array = branchZone.Split(',');
            string zonetoarray = string.Join("','", zone_array.Skip(0));
            string brlist = "select * from BRANCH_LISTS where flag=1 and BrCode in('" + zonetoarray + "');";
            dt= mutility.dbResult(brlist);
            return dt;
           

        }
        public DataTable dtDaily()
        {
            DataTable dt = new DataTable();
            string SQL = @"declare @ReportDate datetime
                        set @ReportDate='2019-04-30'

                        if object_id('tempdb..#WO') is not null
	                        drop table #WO

                        select 
	                        bl.Brletter,
	                        bl.BrCode,
	                        c.CollectDate,
	                        sum(case 
			                        when isnull(([dbo].[fn_GetTotalCurrBalAmtWO](c.BrCode,c.Acc,@ReportDate)),0)<0 then 0
		                        else 
			                        isnull(([dbo].[fn_GetTotalCurrBalAmtWO](c.BrCode,c.Acc,@ReportDate)),0)
		                        end) as AmtToClose,
	                        sum(c.TotalCollectedAmt) as TotalCollectedAmt
	                        into #WO
	                        from [dbo].[WO_COLLECTION] c
	                        inner join [dbo].[BRANCH_LISTS] bl on c.BrCode=bl.BrCode
	                         where reportdate='2019-04-30' and 
	                        collectdate between '2019-04-01' and '2019-04-30'
	                        group by bl.Brletter,bl.BrCode,c.CollectDate
	
	                        if object_id('tempdb..#WOGL') is not null
	                        drop table #WOGL

	                        select 
	                        BrCode,
	                        Brshort,
	                        Postdate,
	                        sum(DrAmt) as ADJCollection,
	                        sum(CrAmt) as TotalCollect 
	                        into #WOGL
	                        from [dbo].[FN_Sum_Cash_movement] where reportdate='2019-04-30' and GLAcc='5744011' group by brcode,Brshort,Postdate

	                        select * from #WO w
	                        left join #WOGL wg on w.BrCode=wg.BrCode";
            dt=mutility.dbResult(SQL);
            return dt;
        }
        //Daily collection
        public ActionResult DailyCollection()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtwrittenoff = new DataTable();
            
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
                    ViewBag.dt_ManageZone = this.brlist(userkey);
                    ViewBag.CurrRunDate = CurrRunDate.CurrRunDate(userkey);
                    ViewBag.Data = this.dtDaily();
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }


            }
        }

        //Monthly collection
        public ActionResult MonthlyCollection()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtwrittenoff = new DataTable();
           
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
                    ViewBag.dt_ManageZone = this.brlist(userkey);
                    ViewBag.CurrRunDate = CurrRunDate.CurrRunDate(userkey);
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }


            }
        }

        //Monthly collection
        public ActionResult SummaryReports()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtwrittenoff = new DataTable();
          
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
                    ViewBag.dt_ManageZone = this.brlist(userkey);
                    ViewBag.CurrRunDate = CurrRunDate.CurrRunDate(userkey);
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
    
    
   
    