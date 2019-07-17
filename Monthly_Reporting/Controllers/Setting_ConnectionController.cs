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
    public class Setting_ConnectionController : Controller
    {
        SqlConnection SqlConn = new SqlConnection();
        Utility mutility = new Utility();
        
        [HttpGet]
        // GET: Setting_Connection
        public ActionResult Index()
        { 
            return View();
        }
        public ActionResult verify_connection()
        {
    
             return RedirectToAction("index","Home");

        }
        [HttpPost]
        public ActionResult SetConnection(Utility utilies)
        {
           
            Settings.Default["DbName"] = utilies.DbName;
            Settings.Default["UserName"] = utilies.UserName;
            Settings.Default["Password"] =utilies.Password;
            Settings.Default["ServerName"] =utilies.ServerName;
            Settings.Default["Remember"] = "1";
            Settings.Default.Save();
            return RedirectToAction("index", "Home");
            
            
        }
        [HttpPost]
       
        public ActResult ExecuteTable(string SQL, SqlConnection sqlConn, SqlTransaction Trans, out DataTable dt)
        {
            ActResult actResult = new ActResult();
            dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.Connection = sqlConn;
            command.Transaction = Trans;
            command.CommandText = SQL;

            SqlDataAdapter adpter = new SqlDataAdapter(command);
            try
            {
                adpter.Fill(dt);
                actResult.Result = ActResult.ResultType.Success;
            }
            catch (Exception)
            {
                actResult.Result = ActResult.ResultType.Fail;
            }

            return actResult;
        }

        public string Select_db(string ServerName,string UserName,string Password)
        {
            string DBName_display = "";
            ActResult xresult = new ActResult();
            SqlConn.ConnectionString = @"Server=" + ServerName.Trim() + ";User Id=" + UserName.Trim() + ";Password=" + Password.Trim();
            try
            {
                if (SqlConn.State != ConnectionState.Open)
                {
                    SqlConn.Open();
                }
                DataTable dt = new DataTable();
                string SQL = @"select name from sys.databases where name not in('master','tempdb','model','msdb','AdventureWorksDW','AdventureWorks') and name='REPORTING_DB'";
                xresult=ExecuteTable(SQL, SqlConn,null,out dt);
                
                if (xresult.Result == ActResult.ResultType.Success)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach(DataRow dr in dt.Rows)
                        {
                            DBName_display += "<option value=" + dr["name"].ToString() + ">" + dr["name"].ToString() + "</option>";
                        }
                       
                    }
                    
                }
                SqlConn.Close();
               
            }
            catch (Exception)
            {
                
                throw;

            }
            return DBName_display;
        }
        
    }
}