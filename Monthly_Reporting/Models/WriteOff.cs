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
    public class WriteOff
    {
        Utility utili = new Utility();
        public DataTable Write_Off(string sqlstring)
        {
            DataTable dtwriteoff = new DataTable();
            ActResult xresult = new ActResult();
            SqlConnection conn = utili.ServerConnection();
            conn.Open();
            string sql = sqlstring;
            xresult = utili.ExecuteTable(sql, conn, null, out dtwriteoff);
            if (xresult.Result == ActResult.ResultType.Success)
            {
                if (dtwriteoff.Rows.Count > 0)
                {
                    return dtwriteoff;
                }
                else
                {
                    return dtwriteoff;
                }
            }
            return dtwriteoff;
        }

     
        public DataTable CulumnTitleWO()
        {
            DataTable dtColumn = new DataTable();
            ActResult xresult = new ActResult();
            SqlConnection conn = utili.ServerConnection();
            conn.Open();
            string sql = "select * from Rep_ColumnTitle where ReportName='WO' and flag=1";
            xresult = utili.ExecuteTable(sql, conn, null, out dtColumn);
            if (xresult.Result == ActResult.ResultType.Success)
            {
                if (dtColumn.Rows.Count > 0)
                {
                    return dtColumn;
                }
                else
                {
                    return dtColumn;
                }

            }
            return dtColumn;
        }
        public DateTime date_start { get; set;}
    }

}
    
