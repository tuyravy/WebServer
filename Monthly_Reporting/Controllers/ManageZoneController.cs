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
   
    public class ManageZoneController : Controller
    {
        readonly SqlConnection sqlcon = new SqlConnection();
        Utility mutility = new Utility();
        readonly ManageZone rs = new ManageZone();
        // GET: ManageZone
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            DataTable dt_main_menu = new DataTable();
            DataTable dt_ManageZone = new DataTable();
            DataTable table = new DataTable();
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

                   
                    
                    string BrZone = Convert.ToString(mutility.dbSingleResult("select branch_zone from users where user_key='S001'"));
                    var zone_array = BrZone.Split(',');
                    string zonetoarray = string.Join("','", zone_array.Skip(0));
                    string SQl = "select row_number() over(order by BL.BrCode) as id,*  ,1 as checked from BRANCH_LISTS BL inner join VPN V ON V.BrCode=BL.BrCode where BL.flag=1 and BL.BrCode in('"+ zonetoarray + "') union all " +
                        "select row_number() over(order by BL.BrCode) as id,*,0 as checked from BRANCH_LISTS BL inner join VPN V ON V.BrCode=BL.BrCode where BL.flag=1 and BL.BrCode not in('" + zonetoarray + "')";
                    
                    ViewBag.dt_ManageZone = mutility.dbResult(SQl);
                    ViewBag.dt_status = table;
                    
                    return View();

                }
                else
                {
                    return RedirectToAction("Index", "Login/Index");
                }


            }
            
        }
        [HttpPost]
        public ActionResult UpdateZone(ManageZone br)
        {
            string daily = @"UPDATE Users set branch_zone='"+br.BrZone.Replace("[","").Replace("]","").Replace("\"\"", "").Replace("\"", "") + "' where user_key='S001'";
            mutility.ExecuteNoneQueryOLEDB(daily);
            return RedirectToAction("ManageZone", "Index");
        }

        [HttpPost]
        public ActionResult CheckStatus()
        {
            
         
                    DataTable dtstatus = new DataTable();
                    string BrZone_loop= Convert.ToString(mutility.dbSingleResult("select branch_zone from users where user_key='S001'"));
                    var zone_array_loop= BrZone_loop.Split(',');
                    string zonetoarray_loop = string.Join("','", zone_array_loop.Skip(0));
                    string SQl_loop= "select row_number() over(order by BL.BrCode) as id,*  ,1 as checked ,'0' as statusconnect from  BRANCH_LISTS BL inner join VPN V ON V.BrCode=BL.BrCode where BL.flag=1 and BL.BrCode in('" + zonetoarray_loop + "')";
                    dtstatus = mutility.dbResult(SQl_loop);
                    SqlConnection sqlcon = new SqlConnection();
                    string StrStatus = "";
                    DataSet ds = new DataSet();
                    DataTable table = new DataTable();
                    DataColumn column;
                    DataRow row;
                    DataView view;
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "BrCode";
                    table.Columns.Add(column);

                    // Create second column.
                    column = new DataColumn();
                    column.DataType = Type.GetType("System.String");
                    column.ColumnName = "statusconnect";
                    table.Columns.Add(column);
                    foreach (DataRow dr in dtstatus.Rows)
                    {
                        sqlcon = mutility.BrConnection(dr["FullDbName"].ToString(), "sa", "Sa@#$Mbwin", dr["VPN"].ToString());
                        if (sqlcon.State != ConnectionState.Open)
                        {
                            try
                            {
                                sqlcon.Open();
                                StrStatus = "Open";
                                row = table.NewRow();
                                row["BrCode"] = dr["BrCode"].ToString();
                                row["statusconnect"] = StrStatus;
                                table.Rows.Add(row);
                            }
                            catch (Exception)
                            {
                                StrStatus = "Close";
                                row = table.NewRow();
                                row["BrCode"] = dr["BrCode"].ToString();
                                row["statusconnect"] = StrStatus;
                                table.Rows.Add(row);

                            }
                        }
                        else
                        {
                            StrStatus = "Close";
                            row = table.NewRow();
                            row["BrCode"] = dr["BrCode"].ToString();
                            row["statusconnect"] = StrStatus;
                            table.Rows.Add(row);
                        }
                    }                    
                   return Json(table, JsonRequestBehavior.AllowGet);
        }
    }
}