using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Monthly_Reporting.Models;
using System.Web.Mvc;
using System.Data;
using Monthly_Reporting.Properties;
using System.Configuration;
namespace Monthly_Reporting.Controllers
{
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
    public class UserAccessRightController : Controller
    {
        SqlConnection con = new SqlConnection();
        DataTable Menu = new DataTable();
        Utility mutility = new Utility();

        // GET: UserAccessRight
        public ActionResult Index()
        {
            return View();
        }

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
        public DataTable UserAccessRight(string userkey)
        {
            ActResult xresult = new ActResult();
            ActResult xresult1 = new ActResult();
            ActResult xresult2= new ActResult();

            DataTable MainMenu = new DataTable();
            DataTable Sub_menu = new DataTable();
            DataTable Sum_in_array = new DataTable();
            DataTable temp_dt = new DataTable();
            DataSet ds = new DataSet();
            int ds_check = 0;
            /*
             * Declare to connection string
             */

            mutility.DbName = Settings.Default["DbName"].ToString();
            mutility.UserName = Settings.Default["UserName"].ToString();
            mutility.Password = Settings.Default["Password"].ToString();
            mutility.ServerName = Settings.Default["ServerName"].ToString();
            //con.ConnectionString = "Data source=" + mutility.ServerName + ";Initial catalog=" + mutility.DbName + ";User ID=" + mutility.UserName + ";Password=" + mutility.Password;

            con=mutility.ServerConnection();

            /*
             * Open connection 
             */
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            //Get main menu
            string Sql_Main =@"
                    SELECT * FROM Sys_Permissions_role Spr
                    INNER JOIN Sys_function_permissions Sfp
                    on Spr.functype = Sfp.functype
                    where Sfp.menulist = 'T' and Spr.user_key = '"+ userkey + "' and Spr.flag = 1";
   
            xresult = ExecuteTable(Sql_Main,con,null,out MainMenu);
            //ds.Tables.Add(MainMenu);
            string SqL_SubMain = @"
                    SELECT * FROM Sys_Permissions_role Spr
                    INNER JOIN Sys_function_permissions Sfp
                    on Spr.functype = Sfp.functype
                    where Sfp.menulist = 'T' and Spr.user_key = '"+ userkey + "' and Spr.flag =2";
            xresult1 = ExecuteTable(SqL_SubMain, con, null, out Sub_menu);
          
            if (xresult.Result == ActResult.ResultType.Success)
            {
                if (Sub_menu.Rows.Count > 0)
                {
                    foreach (DataRow dr in MainMenu.Rows)
                    {

                        string SQl = @"
                    SELECT * FROM Sys_function_permissions Sfp
                    INNER JOIN Sys_permissions_role Spr
                    on Spr.functype = Sfp.functype
                    where Sfp.menulist = 'T' 
                    and Spr.user_key = '" + userkey + @"' 
                    and Sfp.flag =1
                    and Spr.flag!=0
                    and Sfp.parent_id='" + dr["functype"].ToString() + @"'
                    ORDER BY Sfp.order_key ASC";
                        xresult2 = ExecuteTable(SQl, con, null, out Sum_in_array);
                        if (xresult2.Result == ActResult.ResultType.Success)
                        {
                            ds.Tables.Add(Sum_in_array);
                            ds_check = 0;
                        }

                    }


                }
                else
                {
                    ds_check = 1;
                }


            }
            con.Close();

            if (ds_check == 0)
            {
                Menu = ds.Tables[0].Copy();
                for (int i = 1; i < ds.Tables.Count; i++)
                {
                    Menu.Merge(ds.Tables[i].Copy());
                }
            }
           
           
            return Menu;
        }

        public DataTable Main_Menu(string userkey)
        {
            ActResult xresult = new ActResult();
            ActResult xresult1 = new ActResult();
            ActResult xresult2 = new ActResult();

            DataTable MainMenu = new DataTable();
            DataTable Sub_menu = new DataTable();
            DataTable Sum_in_array = new DataTable();
            DataTable temp_dt = new DataTable();
            DataSet ds = new DataSet();
            /*
             * Declare to connection string
             */

            mutility.DbName = Settings.Default["DbName"].ToString();
            mutility.UserName = Settings.Default["UserName"].ToString();
            mutility.Password = Settings.Default["Password"].ToString();
            mutility.ServerName = Settings.Default["ServerName"].ToString();
            con.ConnectionString = "Data source=" + mutility.ServerName + ";Initial catalog=" + mutility.DbName + ";User ID=" + mutility.UserName + ";Password=" + mutility.Password;



            /*
             * Open connection 
             */
            con.Open();

            //Get main menu
            string Sql_Main = @"
                    SELECT * FROM Sys_Permissions_role Spr
                    INNER JOIN Sys_function_permissions Sfp
                    on Spr.functype = Sfp.functype
                    where Sfp.menulist = 'T' and Spr.user_key = '"+ userkey + "' and Spr.flag = 1";

            xresult = ExecuteTable(Sql_Main, con, null, out MainMenu);
            ds.Tables.Add(MainMenu);
            Menu = ds.Tables[0].Copy();
            con.Close();
            return Menu;

        }
    }
}