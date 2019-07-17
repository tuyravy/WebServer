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
    public class ReportDateController : Controller
    {
        SqlConnection sqlcon = new SqlConnection();
        Utility mutility = new Utility();
        // GET: ReportDate
        public DateTime CurrRunDate(string UserKey)
        {
            DateTime CurrRunDate = new DateTime();

            //Get optionReportDate
            string reportdate = "select  replace(reportdate,' ','') as reportdate from Users where user_key = '" + UserKey + "'";
            string optionReportDate = Convert.ToString(mutility.dbSingleResult(reportdate));
            string SQL = "";
            if (optionReportDate.Replace(" ","")== "T")
            {
                 SQL= @"select value_date as dateworking 
                        from Users where flag = 1 and user_key='"+ UserKey +"'";
            }
            else
            {
                 SQL= @"select max(dateworking) as dateworking 
                        from Sys_workingday
                        where flag = 1 and statukey = 1 AND dateworking<=GETDATE();
                    ";
            }
            DataTable dt_Report = new DataTable();
            dt_Report= mutility.dbResult(SQL);
            foreach(DataRow dr in dt_Report.Rows)
            {
                CurrRunDate= DateTime.Parse(dr["dateworking"].ToString());
            }
            


            return CurrRunDate;
        }
    }
}