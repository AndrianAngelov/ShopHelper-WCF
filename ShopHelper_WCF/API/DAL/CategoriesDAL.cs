using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ShopHelper_WCF.API.DTO;
using System.Data;

namespace ShopHelper_WCF.API.DAL
{
    public class CategoriesDAL : BaseDAL
    {
        private string resultMessageCategories;

        public List<CategoryDTO> GetAllCategories()
        {
            SqlConnection dbCon = GetSqlConnection();
            DataTable dt = new DataTable("Categories");
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand selectCommand = new SqlCommand();
            selectCommand.Connection = dbCon;
            selectCommand.CommandText = "SELECT CategoryName FROM Categories";
            da.SelectCommand = selectCommand;
            da.Fill(dt);

            List<CategoryDTO> categoriesList = new List<CategoryDTO>();
            foreach (DataRow row in dt.Rows)
            {
                CategoryDTO currentCategory = new CategoryDTO()
                {
                    CategoryName = row["CategoryName"].ToString(),
                };
                categoriesList.Add(currentCategory);
            }

            return categoriesList;
        }

        public void InsertNewCategory(string categoryName)
        {
            try
            {
                SqlTransaction trans = GetNewTransaction();
                SqlCommand cmd = GetSqlCommand(trans);
                SqlParameterCollection parameters = cmd.Parameters;

                try
                {
                    cmd.CommandText = "INSERT INTO Categories(CategoryName) VALUES(@CategoryName)";
                    parameters.AddWithValue("@CategoryName", categoryName);
                    int record = (int)cmd.ExecuteNonQuery();

                    if (record > 0)
                    {
                        trans.Commit();
                        this.resultMessageCategories = Messages(MessageFor.SucssesInsert);
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    this.resultMessageCategories = Messages(MessageFor.TransactionCancel) + ex.Message;
                }
            }
            finally
            {
                CloseSqlConnection();
            }
        }

        public void UpdateCategory(string categoryOldName, string categoryNewName)
        {
            try
            {
                SqlTransaction trans = GetNewTransaction();
                SqlCommand cmd = GetSqlCommand(trans);
                SqlParameterCollection parameters = cmd.Parameters;
                try
                {
                    int categoryID;

                    cmd.CommandText = "SELECT CategoryID FROM Categories WHERE CategoryName= @ExsistCategoryName";
                    parameters.AddWithValue("@ExsistCategoryName", categoryOldName);
                    categoryID = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "UPDATE Categories SET CategoryName=@CategoryNewName WHERE CategoryID=@CategoryID";
                    parameters.AddWithValue("@CategoryNewName", categoryNewName);
                    parameters.AddWithValue("@CategoryID", categoryID);
                    int record = (int)cmd.ExecuteNonQuery();

                    if (record > 0)
                    {
                        trans.Commit();
                        this.resultMessageCategories = Messages(MessageFor.SucssesUpdate);
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    this.resultMessageCategories = Messages(MessageFor.TransactionCancel) + ex.Message;
                }
            }
            finally
            {
                CloseSqlConnection();
            }
        }

        public void DeleteCategory(string categoryName)
        {
            try
            {
                SqlTransaction trans = GetNewTransaction();
                SqlCommand cmd = GetSqlCommand(trans);
                SqlParameterCollection parameters = cmd.Parameters;

                try
                {
                    int record=0;
                    int categoryID;

                    cmd.CommandText = "SELECT CategoryID FROM Categories WHERE CategoryName= @ExsistCategory";
                    parameters.AddWithValue("@ExsistCategory", categoryName);
                    categoryID = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT COUNT(CategoryID) FROM Products WHERE CategoryID= @CurrentCategoryID";
                    parameters.AddWithValue("@CurrentCategoryID", categoryID);
                    int countProductsWithThisCategory = (int)cmd.ExecuteScalar();

                    if (countProductsWithThisCategory > 0)
                    {
                        cmd.CommandText = "DELETE FROM Products WHERE CategoryID=@CategoryIDinProducts";
                        parameters.AddWithValue("@CategoryIDinProducts", categoryID);
                        record = (int)cmd.ExecuteNonQuery();
                    }

                    cmd.CommandText = "DELETE FROM Categories WHERE CategoryID=@CategoryID";
                    parameters.AddWithValue("@CategoryID", categoryID);
                    record += (int)cmd.ExecuteNonQuery();

                    if (record >= 1)
                    {
                        trans.Commit();
                        this.resultMessageCategories = Messages(MessageFor.SucssesDelete);
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    this.resultMessageCategories = Messages(MessageFor.TransactionCancel) + "\n" + ex.Message;
                }

            }
            finally
            {
                CloseSqlConnection();
            }
        }

        public bool IsCategoryNameExistInDb(string categoryName)
        {
            return IsValueExistsInDB("Categories", "CategoryName", categoryName);
        }

        public override string GetCRUDResult()
        {
            return resultMessageCategories;
        }
    }
}