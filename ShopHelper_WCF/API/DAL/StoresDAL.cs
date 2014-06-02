using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using ShopHelper_WCF.API.DTO;

namespace ShopHelper_WCF.API.DAL
{
    public class StoresDAL:BaseDAL
    {
        public List<StoreDTO> GetAllStores()
        {
            SqlConnection dbCon = GetSqlConnection();
            DataTable dt = new DataTable("Stores");
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand selectCommand = new SqlCommand();
            selectCommand.Connection = dbCon;
            selectCommand.CommandText = "SELECT StoreName FROM Stores";
            da.SelectCommand = selectCommand;
            da.Fill(dt);

            List<StoreDTO> storesList = new List<StoreDTO>();
            foreach (DataRow row in dt.Rows)
            {
                StoreDTO currentStore = new StoreDTO()
                {
                    StoreName = row["StoreName"].ToString(),
                };
                storesList.Add(currentStore);
            }
            return storesList;
        }
    }
}