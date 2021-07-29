using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;                      //利用Data資料庫欄位設定
using System.Data.SqlClient;            //取得連線
using System.Configuration;

namespace AccountingNote.DBsourse
{
    public class UserInfoManager
    {
        public static string GetConnectionString()
        {   
            //Configuration 使用特定資源或應用程式 且不能繼承 ex : Application
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }
        public static DataRow GetUserInfoByAccount(string account)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                @" SELECT [ID], [Account], [PWD], [Name], [Email] 
                    FROM UserInfo 
                    WHERE [Account] = @account
                ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@account", account);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();         //查資料

                        DataTable dt = new DataTable();         //放到DataTable
                        dt.Load(reader);
                        reader.Close();

                        if (dt.Rows.Count == 0)     //沒值回傳N
                            return null;

                        DataRow dr = dt.Rows[0];    //有值回傳第0筆(實際的1)
                        return dr;
                    }
                    catch (Exception ex)
                    {
                        //Web環境無法Console 所以用Logger替
                        Logger.WriteLog(ex);
                        return null;
                    }
                }
            }
        }
    }
}
