using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace Monthly_Reporting.Models
{
    public class PostBackDate
    {
        Utility utili = new Utility();
        public DataTable PostBack_Date(string sqlstring)
        {
            DataTable dtbackdate = new DataTable();
            ActResult xresult = new ActResult();
            SqlConnection conn = utili.ServerConnection();
            conn.Open();
            string sql = sqlstring;
            xresult = utili.ExecuteTable(sql, conn, null, out dtbackdate);
            if (xresult.Result == ActResult.ResultType.Success)
            {
                if (dtbackdate.Rows.Count > 0)
                {
                    return dtbackdate;
                }
                else
                {
                    return dtbackdate;
                }
            }
            return dtbackdate;
        }
        public DateTime date_start { get; set; }
    }

}