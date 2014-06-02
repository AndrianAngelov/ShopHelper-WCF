using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopHelper_WCF.API.DTO;
using System.Data.SqlClient;
using System.Data;

namespace ShopHelper_WCF.API.DAL
{
    public class TownsDAL:BaseDAL
    {
        public List<TownDTO> GetAllTowns()
        {
            SqlConnection dbCon = GetSqlConnection();
            DataTable dt = new DataTable("Towns");
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand selectCommand = new SqlCommand();
            selectCommand.Connection = dbCon;
            selectCommand.CommandText = "SELECT TownName FROM Towns";
            da.SelectCommand = selectCommand;
            da.Fill(dt);

            List<TownDTO> townsList = new List<TownDTO>();
            foreach (DataRow row in dt.Rows)
            {
                TownDTO currentTown = new TownDTO()
                {
                    TownName = row["TownName"].ToString(),
                };
                townsList.Add(currentTown);
            }
            return townsList;
        }
    }
}