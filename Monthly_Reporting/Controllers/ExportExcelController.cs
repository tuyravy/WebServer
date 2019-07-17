using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Monthly_Reporting.Models;
using Monthly_Reporting.Properties;
using OfficeOpenXml;

namespace Monthly_Reporting.Controllers
{    
    public class ExportExcelController : Controller
    {
        readonly Reports rs = new Reports();
        SqlConnection sqlcon = new SqlConnection();
        Utility mutility = new Utility();
        // GET: ExportExcel
        [HttpPost]
        public ActionResult Index(Reports rs)
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

                    string branchZone = Convert.ToString(Session["branch_zone"]);
                    var zone_array = branchZone.Split(',');
                    string zonetoarray = string.Join("','", zone_array.Skip(0));

                    string Sql = "select * from Table_Reports where Report_Code in('" + restOfArray + "')";
                    ViewBag.dt_Report = mutility.dbResult(Sql);
                    string brlist = "select * from BRANCH_LISTS where flag=1 and BrCode in('" + zonetoarray + "');";
                    ViewBag.dt_ManageZone = mutility.dbResult(brlist);

                    string dtExcute = "";
                    DataTable dtAffterEx = new DataTable();
                    string ReExcuteName = "select StrSql from Table_Reports where Report_Code='" + rs.ReportCode.Trim() + "'";
                    dtExcute = Convert.ToString(mutility.dbSingleResult(ReExcuteName));

                    string Run = dtExcute;
                    ViewBag.dtRun = mutility.dbResult(Run);
                

                    ExcelPackage pck = new ExcelPackage();
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
            
                    ws.Cells["A1"].Value = "Communication";
                    ws.Cells["B1"].Value = "Com1";

                    ws.Cells["A2"].Value = "Report";
                    ws.Cells["B2"].Value = "Report1";

                    ws.Cells["A3"].Value = "Date";
                    ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

                    ws.Cells["A6"].Value = "EmployeeId";
                    ws.Cells["B6"].Value = "EmployeeName";
                    ws.Cells["C6"].Value = "Email";
                    ws.Cells["D6"].Value = "Phone";
                    ws.Cells["E6"].Value = "Experience";

                    int rowStart = 7;
                    foreach (DataRow  item in dtRun.Rows)
                    {
                        for (int i = 0; i <= 10; i++)
                        {

                            ws.Cells[string.Format("A{0}", rowStart)].Value = item[i].ToString();
                            ws.Cells[string.Format("B{0}", rowStart)].Value = item[i].ToString();
                            ws.Cells[string.Format("C{0}", rowStart)].Value = item[i].ToString();
                            ws.Cells[string.Format("D{0}", rowStart)].Value = item[i].ToString();
                            ws.Cells[string.Format("E{0}", rowStart)].Value = item[i].ToString();
                        }
                        rowStart++;
                    }

                    ws.Cells["A:AZ"].AutoFitColumns();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
                    Response.BinaryWrite(pck.GetAsByteArray());
                    Response.End();



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
            
           