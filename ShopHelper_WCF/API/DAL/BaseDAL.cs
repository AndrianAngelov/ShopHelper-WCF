using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace ShopHelper_WCF.API.DAL
{
    public enum MessageFor
    {
        SucssesInsert,
        SucssesUpdate,
        SucssesDelete,
        ItemExistInDatabase,
        TransactionCancel,
    }

    public class BaseDAL
    {
        private readonly static string dbConnectionString = ConfigurationManager.ConnectionStrings["ShopHelperDatabase"].ConnectionString;
        private static SqlConnection dbCon;

            
        public static SqlConnection GetSqlConnection(string databaseConnectionString)
        {
            try
            {
                if (databaseConnectionString == null)
                {
                    dbCon = new SqlConnection(dbConnectionString);
                }
                else
                {
                    dbCon = new SqlConnection(databaseConnectionString);
                }

                return dbCon;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static SqlConnection GetSqlConnection()
        {
            return GetSqlConnection(null);
        }

        public static void CloseSqlConnection()
        {
            try
            {
                if (dbCon != null)
                {
                    dbCon.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static SqlTransaction GetNewTransaction()
        {
            dbCon = GetSqlConnection();
            if (dbCon.State == ConnectionState.Closed)
            {
                dbCon.Open();
            }
            return dbCon.BeginTransaction();
        }

        public static SqlCommand GetSqlCommand(SqlTransaction transactionName)
        {
            SqlCommand newCommand = dbCon.CreateCommand();
            newCommand.Transaction = transactionName;
            return newCommand;
        }

        protected bool IsValueExistsInDB(string tableName, string columName, object value)
        {
            dbCon = new SqlConnection(dbConnectionString);
            SqlCommand cmd = dbCon.CreateCommand();
            SqlParameterCollection parameters = cmd.Parameters;
            bool result = false;

            cmd.CommandText = @"SELECT COUNT(" + columName + ") FROM " + tableName + " WHERE " + columName + "= @value";
            parameters.AddWithValue("@value", value);

            try
            {
                dbCon.Open();
                int counter = (int)cmd.ExecuteScalar();
                dbCon.Close();
                if (counter == 1 || counter > 1)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public virtual string GetCRUDResult()
        {
            string currentResultMessage = "Some current message";
            return currentResultMessage;
        }

        public static string Messages(MessageFor messageTextFor,string currentItemName="")
        {
            string messageText="";
            switch (messageTextFor)
            {
                case MessageFor.SucssesInsert:
                    messageText = "Succsses Insert";
                    break;
                case MessageFor.SucssesUpdate:
                    messageText = "Succsses Update";
                    break;
                case MessageFor.SucssesDelete:
                    messageText = "Succsses Delete";
                    break;
                case MessageFor.ItemExistInDatabase:
                    messageText = "" + currentItemName + " alreday exist.Please enter new " + currentItemName + " Name";
                    break;
                case MessageFor.TransactionCancel:
                    messageText = "Transaction cancelled";
                    break;
            }
            return messageText;
        }
    }
}