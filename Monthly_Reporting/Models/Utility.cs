using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Monthly_Reporting.Properties;
using System.Configuration;
using System.Data.OleDb;

namespace Monthly_Reporting.Models
{
    public class Utility
    {
        SqlConnection con = new SqlConnection();
        //Branch Conneciton
        public SqlConnection LocalConnection()
        {
            SqlConnection con = new SqlConnection();
            DbName = Settings.Default["DbName"].ToString();
            UserName = Settings.Default["UserName"].ToString();
            Password = Settings.Default["Password"].ToString();
            ServerName = Settings.Default["ServerName"].ToString();
            con.ConnectionString = "Data source=" + ServerName + ";Initial catalog=" + DbName + ";User ID=" + UserName + ";Password=" + Password;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            return con;

        }
        public ActResult ExecuteNoneQueryOLEDB(string SQL)
        {
            
            ActResult _result = new ActResult();
            SqlConnection sqlcon = LocalConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = SQL;
            command.CommandType = CommandType.Text;
            command.Connection = sqlcon;
            try
            {
                if (sqlcon.State != ConnectionState.Open)
                {
                    sqlcon.Open();
                }
                command.ExecuteNonQuery();
                _result.Result = ActResult.ResultType.Success;
            }
            catch (Exception ex)
            {
                //Console.WriteLine("\r\n Error to Execute Command \r\n" + ex.Message);
                _result.Message = "\r\n Error to Execute Command \r\n" + ex.Message;
                _result.Result = ActResult.ResultType.Fail;
            }
            finally
            {
                sqlcon.Close();
            }
            return _result;
        }
        //Server Connection or HO Connection
        public SqlConnection ServerConnection()
        {
            SqlConnection con = new SqlConnection();
            DbName = Settings.Default["DbName"].ToString();
            UserName = Settings.Default["UserName"].ToString();
            Password = Settings.Default["Password"].ToString();
            ServerName = Settings.Default["ServerName"].ToString();
            con.ConnectionString = "Data source=" + ServerName + ";Initial catalog=" + DbName + ";User ID=" + UserName + ";Password=" + Password;           
            return con;
        }
       //Branch Connection from Branch to HO
       public SqlConnection BrConnection(string DbName,string UserName,string Password,string ServerName)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data source=" + ServerName + ";Initial catalog=" + DbName + ";User ID=" + UserName + ";Password=" + Password;
          
            return con;
        }

        public string ServerName { get; set; }
        public string DbName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

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
        public DataTable dbBranchResult(string sqlstring,SqlConnection sqlConn)
        {
            DataTable dtResult = new DataTable();
            ActResult xresult = new ActResult();
            SqlConnection conn =sqlConn;
            conn.Open();
            string sql = sqlstring;
            xresult = ExecuteTable(sql, conn, null, out dtResult);
            if (xresult.Result == ActResult.ResultType.Success)
            {
                if (dtResult.Rows.Count > 0)
                {
                    return dtResult;
                }
                else
                {
                    return dtResult;
                }
            }
            return dtResult;
        }
        public DataTable dbResult(string sqlstring)
        {
            DataTable dtResult = new DataTable();
            ActResult xresult = new ActResult();
            SqlConnection conn = ServerConnection();
            conn.Open();
            string sql = sqlstring;
            xresult = ExecuteTable(sql, conn, null, out dtResult);
            if (xresult.Result == ActResult.ResultType.Success)
            {
                if (dtResult.Rows.Count > 0)
                {
                    return dtResult;
                }
                else
                {
                    return dtResult;
                }
            }
            return dtResult;
        }
        public string dbSingleResult(string sqlstring)
        {
            DataTable dtResult = new DataTable();
            ActResult xresult = new ActResult();
            SqlConnection conn = ServerConnection();
            string StrSql = "";
            conn.Open();
            string sql = sqlstring;
            xresult = ExecuteTable(sql, conn, null, out dtResult);
            if (xresult.Result == ActResult.ResultType.Success)
            {
                if (dtResult.Rows.Count > 0)
                {
                     foreach(DataRow dr in dtResult.Rows)
                    {
                        StrSql=dr[0].ToString();
                    }  
                }
                
            }
            return StrSql;
        }
        //public ActResult ExecuteTable(string SQL, SqlConnection sqlcon, out DataTable dt)
        //{
        //    dt = new DataTable();
        //    ActResult _result = new ActResult();
        //    SqlCommand command = new SqlCommand();
        //    command.CommandType = CommandType.Text;
        //    command.Connection = sqlcon;
        //    command.CommandText = SQL;
        //    SqlDataAdapter adpter = new SqlDataAdapter(command);
        //    try
        //    {
        //        adpter.Fill(dt);
        //        _result.Result = ActResult.ResultType.Success;
        //    }
        //    catch (Exception)
        //    {
        //        _result.Result = ActResult.ResultType.Fail;

        //    }

        //    return _result;
        //}
    }

    public class ActResult
    {
        public ResultType Result { get; set; }
        public enum ResultType
        {
            Success = 1,
            Fail = 2
        }
        public string Message { get; set; }
    }
}