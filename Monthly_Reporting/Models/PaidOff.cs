using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Monthly_Reporting.Properties;
using System.Configuration;
namespace Monthly_Reporting.Models
{
    public class PaidOff
    {
        Utility utili = new Utility();
        public DataTable paidoff(string concat)
        {
            DataTable dtpaidoff = new DataTable();
            ActResult xresult = new ActResult();
            SqlConnection conn = utili.ServerConnection();
            conn.Open();
            string sql = @"select * from dbo.Rep_Client_PaidOff where reportdate='2019-04-30' and "+ concat + " order by BrCode DESC";
            xresult = utili.ExecuteTable(sql,conn,null,out dtpaidoff);
            if (xresult.Result == ActResult.ResultType.Success)
            {
                if (dtpaidoff.Rows.Count > 0)
                {
                    return dtpaidoff;
                }
                else
                {
                    return dtpaidoff;
                }
               
            }
            return dtpaidoff;
        }
        public DateTime date_start { get; set; }
    }

}