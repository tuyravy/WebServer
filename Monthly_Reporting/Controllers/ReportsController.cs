using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using Monthly_Reporting.Models;
using Monthly_Reporting.Properties;
using OfficeOpenXml;
namespace Monthly_Reporting.Controllers
{
    public class ReportsController : Controller
    {
        readonly SqlConnection sqlcon = new SqlConnection();
        Utility mutility = new Utility();
        readonly Reports rs = new Reports();
        // GET: Reports
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dt_Report = new DataTable();
            DataTable dtRun = new DataTable();
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
                    string userkey = Convert.ToString(Session["user_key"]);
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    string theString = Convert.ToString(Session["report_code"]);
                    var array = theString.Split(',');
                    string firstElem = array.First();
                    string restOfArray = string.Join("','", array.Skip(0));

                    string branchZone = Convert.ToString(mutility.dbSingleResult("select branch_zone from users where user_key='S001'"));

                    var zone_array = branchZone.Split(','); 
                    string zonetoarray = string.Join("','", zone_array.Skip(0));
                    string Sql = "select * from Table_Reports where flag=1 and Report_Code in('"+ restOfArray + "')";
                    ViewBag.dt_Report = mutility.dbResult(Sql);
                    string brlist = "select * from BRANCH_LISTS where flag=1 and BrCode in('"+ zonetoarray + "');";
                    ViewBag.dt_ManageZone = mutility.dbResult(brlist);
                    ViewBag.dtRun = dtRun;
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }
            }
        }
        [HttpPost]
        public ActionResult ExecuteScript(Reports rs)
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dt_Report = new DataTable();
            DataTable dtRun = new DataTable();
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
                    string userkey = Convert.ToString(Session["user_key"]);
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    string theString = Convert.ToString(Session["report_code"]);
                    var array = theString.Split(',');
                    string firstElem = array.First();
                    string restOfArray = string.Join("','", array.Skip(0));

                    
                    string Sql = "select replace(Report_Code,' ','') as Report_Code, Name from Table_Reports where flag=1 and Report_Code in('" + restOfArray + "')";
                    ViewBag.dt_Report = mutility.dbResult(Sql);

                    string branchZone = Convert.ToString(mutility.dbSingleResult("select branch_zone from users where user_key='S001'"));
                    var zone_array = branchZone.Split(',');
                    string zonetoarray = string.Join("','", zone_array.Skip(0));
                    string brlist = "select * from BRANCH_LISTS where flag=1 and BrCode in('" + zonetoarray + "');";

                    ViewBag.dt_ManageZone = mutility.dbResult(brlist);

                    string dtExcute = "";
                    DataTable dtAffterEx = new DataTable();              
                    
                  string ReExcuteName = "select StrSql from Table_Reports where Report_Code='" + rs.ReportCode.Trim() + "'";                                          
                    dtExcute = Convert.ToString(mutility.dbSingleResult(ReExcuteName));                    
                    string Run = dtExcute;

                    //Get ReportName
                    string ValueString = "select Name from Table_Reports where Report_Code = '" + rs.ReportCode.Replace(" ", "") + "'";
                    string ReportName = Convert.ToString(mutility.dbSingleResult(ValueString));
                    //Get Branch Name
               
                    string BrName = Convert.ToString(mutility.dbSingleResult("select BrLetter from BRANCH_LISTS where flag=1 and BrCode='" + rs.BrCode+"'"));


                    //Selected data 
                    ViewBag.BrCode = rs.BrCode;
                    ViewBag.ReportCode =rs.ReportCode.Replace(" ","");


                    DataTable dtstatus = new DataTable();
                    string BrZone_loop = Convert.ToString(mutility.dbSingleResult("select branch_zone from users where user_key='S001'"));
                    var zone_array_loop = BrZone_loop.Split(',');
                    string zonetoarray_loop = string.Join("','", zone_array_loop.Skip(0));
                    string SQl_loop = "";
                    if (rs.BrCode== "ALL")
                    {
                        SQl_loop = "select row_number() over(order by BL.BrCode) as id,*  ,1 as checked ,'0' as statusconnect from  BRANCH_LISTS BL inner join VPN V ON V.BrCode=BL.BrCode where BL.flag=1 and  BL.BrCode in('" + zonetoarray_loop + "')";
                    }
                    else
                    {
                         SQl_loop = "select row_number() over(order by BL.BrCode) as id,*  ,1 as checked ,'0' as statusconnect from  BRANCH_LISTS BL inner join VPN V ON V.BrCode=BL.BrCode where BL.flag=1 and BL.BrCode='" + rs.BrCode + "'";//BL.BrCode in('" + zonetoarray_loop + "')
                    }
                    
                    dtstatus = mutility.dbResult(SQl_loop);
                    SqlConnection sqlcon = new SqlConnection();                  
                    DataSet ds = new DataSet();
                    DataTable table = new DataTable();                    
                    foreach (DataRow dr in dtstatus.Rows)
                    {
                        sqlcon = mutility.BrConnection(dr["FullDbName"].ToString(), "sa", "Sa@#$Mbwin", dr["VPN"].ToString());
                        if (sqlcon.State != ConnectionState.Open)
                        {
                            try
                            {
                                dtRun = mutility.dbBranchResult(Run, sqlcon);
                                ds.Tables.Add(dtRun);
                            }
                            catch (Exception){
                            }                          
                        }                        
                    }
                    DataTable publicdt = new DataTable();
                    for(int i=0;i< ds.Tables.Count; i++)
                    {
                        publicdt.Merge(ds.Tables[i]);
                    }
                    if (rs.BrCode == "ALL")
                    {
                        BrName = "All_Branch";
                    }
                    
                    ExportDataToExcel(publicdt, BrName+"_"+ReportName);
                    TempData["sms"] = "You are already donwload reports";

                    return View("Index");                   
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }
            }            
        }

        public void ExportDataToExcel(DataTable dt, string fileName)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt,"sheet1");
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        [HttpGet]
        public ActionResult ExportExcel(string reportcode)
        {            
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dtRun = new DataTable();
            DataTable dt_Report = new DataTable();
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
                    string userkey = Convert.ToString(Session["user_key"]);
                    dt = UsAccRight.UserAccessRight(userkey);
                    dt_main_menu = UsAccRight.Main_Menu(userkey);
                    ViewBag.manin_menu = dt_main_menu;
                    ViewBag.sub_manin = dt;
                    string theString = Convert.ToString(Session["report_code"]);
                    var array = theString.Split(',');
                    string firstElem = array.First();
                    string restOfArray = string.Join("','", array.Skip(0));
                    string branchZone = Convert.ToString(mutility.dbSingleResult("select branch_zone from users where user_key='S001'"));
                    var zone_array = branchZone.Split(',');
                    string zonetoarray = string.Join("','", zone_array.Skip(0));
                    string Sql = "select * from Table_Reports where Report_Code in('" + restOfArray + "')";
                    ViewBag.dt_Report = mutility.dbResult(Sql);
                    string brlist = "select * from BRANCH_LISTS where flag=1 and BrCode in('" + zonetoarray + "');";
                    ViewBag.dt_ManageZone = mutility.dbResult(brlist);
                    string dtExcute = "";
                    DataTable dtAffterEx = new DataTable();
                    string ReExcuteName = "select StrSql from Table_Reports where Report_Code='"+ reportcode + "'";
                    dtExcute = Convert.ToString(mutility.dbSingleResult(ReExcuteName));
                    string Run = dtExcute;
                   dtRun = mutility.dbResult(Run);
                    ViewBag.dtRun = dtRun;
                    XLWorkbook wb = new XLWorkbook();
                    wb.Worksheets.Add(dtRun, "Sheet1");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                    Response.End();
                    return View("index");
                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }
            }
        }
    }
}
        
    
