using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopHelper_WCF.API.DTO;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace ShopHelper_WCF.API.DAL
{
    public class ProductsDAL : BaseDAL
    {
        private string resultMessageProducts;

        public List<ProductDTO> GetAllProducts()
        {
            SqlConnection dbCon = GetSqlConnection();
            DataTable dt = new DataTable("Products");
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand selectCommand = new SqlCommand();
            selectCommand.Connection = dbCon;
            selectCommand.CommandText = @"SELECT p.ProductName ,v.VendorName,p.UnitPrice,p.Quantity,
                                                 c.CategoryName,s.StoreName,t.TownName
                                          FROM Products as p 
                                            JOIN Categories c ON p.CategoryID=c.CategoryID
                                            JOIN Vendors v ON p.VendorID=v.VendorID
                                            JOIN Stores s ON p.StoreID=s.StoreID
                                            JOIN Towns t ON p.TownID=t.TownID  ";

            da.SelectCommand = selectCommand;
            da.Fill(dt);

            List<ProductDTO> productsList = new List<ProductDTO>();
            foreach (DataRow row in dt.Rows)
            {
                ProductDTO p = new ProductDTO()
                {
                    ProductNewName = Convert.ToString(row["ProductName"]),
                    CategoryName = row["CategoryName"].ToString(),
                    VendorName = (row["VendorName"]).ToString(),
                    ProductPrice = (decimal)(row["UnitPrice"]),
                    Quantity = (int)(row["Quantity"]),
                    StoreName = row["StoreName"].ToString(),
                    TownName = row["TownName"].ToString(),
                };
                productsList.Add(p);
            }
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    productsList.Add(new ProductDTO());
            //    //productsList[i] = new ProductDTO();
            //    productsList[i].ProductName = dt.Rows[i].ItemArray[1].ToString();
            //    productsList[i].ProductPrice = (int)dt.Rows[i].ItemArray[2];
            //    productsList[i].VendorName = dt.Rows[i].ItemArray[3].ToString();
            //    //productsList.Add(productsList[i]);
            //}

            return productsList;
        }

        public void InsertNewProduct(string productName, string categoryName, string vendorName,
                                     decimal unitPrice, int quantity, string storeName, string townName)
        {
            try
            {
                SqlTransaction trans = GetNewTransaction();
                SqlCommand cmd = GetSqlCommand(trans);
                SqlParameterCollection parameters = cmd.Parameters;

                try
                {
                    int categoryID;
                    int vendorID;
                    int storeID;
                    int townID;

                    cmd.CommandText = "SELECT CategoryID FROM Categories WHERE CategoryName= @CategoryName";
                    parameters.AddWithValue("@CategoryName", categoryName);
                    categoryID = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT VendorID FROM Vendors WHERE VendorName= @VendorName";
                    parameters.AddWithValue("@VendorName", vendorName);
                    vendorID = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT StoreID FROM Stores WHERE StoreName= @StoreName";
                    parameters.AddWithValue("@StoreName", storeName);
                    storeID = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT TownID FROM Towns WHERE TownName= @TownName";
                    parameters.AddWithValue("@TownName", townName);
                    townID = (int)cmd.ExecuteScalar();

                    cmd.CommandText = @"INSERT INTO Products(ProductName,CategoryID,VendorID,UnitPrice,Quantity,StoreID,TownID)
                                               VALUES(@Product ,@CategoryID, @VendorID,@Price,@Quantity,@StoreID,@TownID)";
                    parameters.AddWithValue("@Product", productName);
                    parameters.AddWithValue("@CategoryID", categoryID);
                    parameters.AddWithValue("@VendorID", vendorID);
                    parameters.AddWithValue("@Price", unitPrice);
                    parameters.AddWithValue("@Quantity", quantity);
                    parameters.AddWithValue("@StoreID", storeID);
                    parameters.AddWithValue("@TownID", townID);
                    int record = (int)cmd.ExecuteNonQuery();

                    if (record > 0)
                    {
                        trans.Commit();
                        this.resultMessageProducts = Messages(MessageFor.SucssesInsert);
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    this.resultMessageProducts = Messages(MessageFor.TransactionCancel) + ex.Message;
                }

            }
            finally
            {
                CloseSqlConnection();
            }
        }

        public void UpdateProduct(string productOldName, string productNewName, string categoryName, string vendorName,
                                  decimal unitPrice, int quantity, string storeName, string townName)
        {
            try
            {
                SqlTransaction trans = GetNewTransaction();
                SqlCommand cmd = GetSqlCommand(trans);
                SqlParameterCollection parameters = cmd.Parameters;

                try
                {
                    int productID;
                    int categoryID;
                    int vendorID;
                    int storeID;
                    int townID;

                    cmd.CommandText = "SELECT ProductID FROM Products WHERE ProductName= @ExsistProduct";
                    parameters.AddWithValue("@ExsistProduct", productOldName);
                    productID = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT CategoryID FROM Categories WHERE CategoryName= @CategoryName";
                    parameters.AddWithValue("@CategoryName", categoryName);
                    categoryID = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT VendorID FROM Vendors WHERE VendorName= @VendorName";
                    parameters.AddWithValue("@VendorName", vendorName);
                    vendorID = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT StoreID FROM Stores WHERE StoreName= @StoreName";
                    parameters.AddWithValue("@StoreName", storeName);
                    storeID = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT TownID FROM Towns WHERE TownName= @TownName";
                    parameters.AddWithValue("@TownName", townName);
                    townID = (int)cmd.ExecuteScalar();

                    cmd.CommandText = @"UPDATE Products 
                                         SET ProductName=@ProductNewName,CategoryID=@CategoryID,VendorID=@VendorID,
                                             UnitPrice=@UnitPrice,Quantity=@Quantity,StoreID=@StoreID,TownID=@TownID
                                         WHERE ProductID=@ProductID";
                    parameters.AddWithValue("@ProductID", productID);
                    parameters.AddWithValue("@ProductNewName", productNewName);
                    parameters.AddWithValue("@CategoryID", categoryID);
                    parameters.AddWithValue("@VendorID", vendorID);
                    parameters.AddWithValue("@UnitPrice", unitPrice);
                    parameters.AddWithValue("@Quantity", quantity);
                    parameters.AddWithValue("@StoreID", storeID);
                    parameters.AddWithValue("@TownID", townID);
                    int record = (int)cmd.ExecuteNonQuery();

                    if (record > 0)
                    {
                        trans.Commit();
                        this.resultMessageProducts = Messages(MessageFor.SucssesUpdate);
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    this.resultMessageProducts = Messages(MessageFor.TransactionCancel) + ex.Message;
                }

            }
            finally
            {
                CloseSqlConnection();
            }
        }

        public void DeleteProduct(string productName)
        {
            try
            {
                SqlTransaction trans = GetNewTransaction();
                SqlCommand cmd = GetSqlCommand(trans);
                SqlParameterCollection parameters = cmd.Parameters;

                try
                {
                    int productID;

                    cmd.CommandText = "SELECT ProductID FROM Products WHERE ProductName= @ExsistProduct";
                    parameters.AddWithValue("@ExsistProduct", productName);
                    productID = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "DELETE FROM Products WHERE ProductID=@ProductID";
                    parameters.AddWithValue("@ProductID", productID);
                    int record = (int)cmd.ExecuteNonQuery();

                    if (record > 0)
                    {
                        trans.Commit();
                        this.resultMessageProducts = Messages(MessageFor.SucssesDelete);
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    this.resultMessageProducts = Messages(MessageFor.TransactionCancel) + ex.Message;
                }

            }
            finally
            {
                CloseSqlConnection();
            }
        }

        public bool IsProductNameExistInDb(string productName)
        {
            return IsValueExistsInDB("Products", "ProductName", productName);
        }

        public override string GetCRUDResult()
        {
            return resultMessageProducts;
        }
    }
}