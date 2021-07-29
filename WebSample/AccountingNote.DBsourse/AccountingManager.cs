using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AccountingNote.DBsourse
{
    public class AccountingManager
    {
        private static string GetConnectionString()
        {
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }
        public static DataTable GetAccountingList(string userID)
        {
            string connectionString = GetConnectionString();
            string dbCommand =
                $@" SELECT
                        ID,
                        Caption,
                        Amount,
                        ActType,
                        CreateDate
                    FROM Accounting
                    WHERE UserID = @userID
                ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommand, connection))
                {
                    command.Parameters.AddWithValue("@userID", userID);
                    try
                    {
                        connection.Open();
                        var reader = command.ExecuteReader();

                        DataTable dt = new DataTable();         //放到DataTable
                        dt.Load(reader);

                        return dt;
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return null;
                    }
                }
            }
        }
        public static DataTable GetAccounting(int id)
        {
            string connectionString = GetConnectionString();
            string dbCommand =
                $@" SELECT
                        ID,
                        Caption,
                        Amount,
                        ActType,
                        CreateDate
                    FROM Accounting
                    WHERE id = @id AND UserID =@userID
                ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommand, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@userID", userID);
                    try
                    {
                        connection.Open();
                        var reader = command.ExecuteReader();

                        DataTable dt = new DataTable();         //放到DataTable
                        dt.Load(reader);

                        return dt;
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// 建立流水線
        /// </summary>
        public static bool UpdateAccounting(int ID, string userID, string caption, int amount, int actType, string body)
        {
            //  check input
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount  must  between  0 and 1,000,000.");
            if (actType <0 || actType > 1)
                throw new ArgumentException("ActType must be 0 or 1");

            string connectionString = GetConnectionString();
            string dbCommand =
                $@" UPDATE [Accounting]
                    SET
                        UserID      =@userID
                        ,Caption    =@caption
                        ,Amount     =@amount
                        ,ActType    =actType
                        ,CreateDate =createDate
                        ,Body       =@body
                    WHERE
                        ID = @id
                ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommand, connection))
                {
                    try
                    {
                        connection.Open();
                        int effectRows = command.ExecuteNonQuery();

                        if (effectRows == 1)
                            return true;
                        else
                            return false;
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return false;
                    }
                }
            }
        }
        public static void DeleteAccounting(int ID)
        {
            string connectionString = GetConnectionString();
            string dbCommand =
                $@" DELETE  [Accounting]
                    WHERE ID = @id ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommand, connection))
                {
                    command.Parameters.AddWithValue("@id",ID);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                    }
                }
            }
        }
        public static void CreateAccounting(string userID, string caption, int amount, int actType, string body)
        {
            //  check input
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount  must  between  0 and 1,000,000.");
            if (actType <0 || actType > 1)
                throw new ArgumentException("ActType must be 0 or 1");

            string connectionString = GetConnectionString();
            string dbCommand =
                $@" INSERT INTO [dbo].[Accounting]
                    (
                        UserID     
                        ,Caption    
                        ,Amount     
                        ,ActType    
                        ,CreateDate 
                        ,Body       
                    )
                    VALUES
                    (
                        @userid    
                        ,@caption    
                        ,@amount     
                        ,@actType    
                        ,@createDate 
                        ,@body       
                    ) ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommand, connection))
                {
                    command.Parameters.AddWithValue("@userID", userID);
                    command.Parameters.AddWithValue("@caption", caption);
                    command.Parameters.AddWithValue("@amount", amount);
                    command.Parameters.AddWithValue("@actType", actType);
                    command.Parameters.AddWithValue("@createDate", DateTime.Now);
                    command.Parameters.AddWithValue("@body", body);
                    command.Parameters.AddWithValue("@id",ID);
                    try
                    {
                        connection.Open();
                        int effectRows = command.ExecuteNonQuery();

                        if (effectRows == 1)
                            return true;
                        else
                            return false;
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return false;
                    }
                }
            }
        }
    }
}
