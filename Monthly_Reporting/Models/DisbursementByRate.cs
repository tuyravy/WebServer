using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Monthly_Reporting.Models
{
    public class DisbursementByRate
    {
        Utility utili = new Utility();
        public DataTable Disb_Rate(string sqlstring)
        {
            DataTable dtdisbRate = new DataTable();
            ActResult xresult = new ActResult();
            SqlConnection conn = utili.ServerConnection();
            conn.Open();
            string sql = sqlstring;
            xresult = utili.ExecuteTable(sql, conn, null, out dtdisbRate);
            if (xresult.Result == ActResult.ResultType.Success)
            {
                if (dtdisbRate.Rows.Count > 0)
                {
                    return dtdisbRate;
                }
                else
                {
                    return dtdisbRate;
                }
            }
            return dtdisbRate;
        }
        public DateTime date_start { get; set; }
    }

}
    
