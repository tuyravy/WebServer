using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
namespace Monthly_Reporting.Models
{
    public class ClientFrequency
    {
        Utility utili = new Utility();
        public DataTable Client_Freq(string sqlstring)
        {
            DataTable dtclientfre = new DataTable();
            ActResult xresult = new ActResult();
            SqlConnection conn = utili.ServerConnection();
            conn.Open();
            string sql = sqlstring;
            xresult = utili.ExecuteTable(sql, conn, null, out dtclientfre);
            if (xresult.Result == ActResult.ResultType.Success)
            {
                if (dtclientfre.Rows.Count > 0)
                {
                    return dtclientfre;
                }
                else
                {
                    return dtclientfre;
                }
            }
            return dtclientfre;
        }
        public DateTime date_start { get; set; }
    }

}
    
