using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopHelper_WCF.API.DTO;
using System.Data.SqlClient;
using System.Data;

namespace ShopHelper_WCF.API.DAL
{
    public class VendorsDAL : BaseDAL
    {
        private string resultMessageVendors;

        public List<VendorDTO> GetAllVendors()
        {
            SqlConnection dbCon = GetSqlConnection();
            DataTable dt = new DataTable("Vendors");
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand selectCommand = new SqlCommand();
            selectCommand.Connection = dbCon;
            selectCommand.CommandText = "SELECT VendorName FROM Vendors";
            da.SelectCommand = selectCommand;
            da.Fill(dt);

            List<VendorDTO> vendorsList = new List<VendorDTO>();
            foreach (DataRow row in dt.Rows)
            {
                VendorDTO currentVendor = new VendorDTO()
                {
                    VendorName = row["VendorName"].ToString(),
                };
                vendorsList.Add(currentVendor);
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

            return vendorsList;
        }

        public void InsertNewVendor(string vendorName)
        {
            try
            {
                SqlTransaction trans = GetNewTransaction();
                SqlCommand cmd = GetSqlCommand(trans);
                SqlParameterCollection parameters = cmd.Parameters;

                try
                {
                    cmd.CommandText = "INSERT INTO Vendors(VendorName) VALUES(@VendorName)";
                    parameters.AddWithValue("@VendorName", vendorName);
                    int record = (int)cmd.ExecuteNonQuery();

                    if (record > 0)
                    {
                        trans.Commit();
                        this.resultMessageVendors = Messages(MessageFor.SucssesInsert);
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    this.resultMessageVendors = Messages(MessageFor.TransactionCancel) + ex.Message;
                }
            }
            finally
            {
                CloseSqlConnection();
            }
        }

        public void UpdateVendor(string vendorOldName, string vendorNewName)
        {
            try
            {
                SqlTransaction trans = GetNewTransaction();
                SqlCommand cmd = GetSqlCommand(trans);
                SqlParameterCollection parameters = cmd.Parameters;
                try
                {
                    int vendorID;

                    cmd.CommandText = "SELECT VendorID FROM Vendors WHERE VendorName= @ExsistVendorName";
                    parameters.AddWithValue("@ExsistVendorName", vendorOldName);
                    vendorID = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "UPDATE Vendors SET VendorName=@VendorNewName WHERE VendorID=@VendorID";
                    parameters.AddWithValue("@VendorNewName", vendorNewName);
                    parameters.AddWithValue("@VendorID", vendorID);
                    int record = (int)cmd.ExecuteNonQuery();

                    if (record > 0)
                    {
                        trans.Commit();
                        this.resultMessageVendors = Messages(MessageFor.SucssesUpdate);
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    this.resultMessageVendors = Messages(MessageFor.TransactionCancel) + ex.Message;
                }
            }
            finally
            {
                CloseSqlConnection();
            }
        }

        public void DeleteVendor(string vendorName)
        {
            try
            {
                SqlTransaction trans = GetNewTransaction();
                SqlCommand cmd = GetSqlCommand(trans);
                SqlParameterCollection parameters = cmd.Parameters;

                try
                {
                    int record = 0;
                    int vendorID;

                    cmd.CommandText = "SELECT VendorID FROM Vendors WHERE VendorName= @ExsistVendor";
                    parameters.AddWithValue("@ExsistVendor", vendorName);
                    vendorID = (int)cmd.ExecuteScalar();

                    cmd.CommandText = "SELECT COUNT(VendorID) FROM Products WHERE VendorID= @CurrentVendorID";
                    parameters.AddWithValue("@CurrentVendorID", vendorID);
                    int countProductsWithThisVendor = (int)cmd.ExecuteScalar();

                    if (countProductsWithThisVendor > 0)
                    {
                        cmd.CommandText = "DELETE FROM Products WHERE VendorID=@VendorIDinProducts";
                        parameters.AddWithValue("@VendorIDinProducts", vendorID);
                        record = (int)cmd.ExecuteNonQuery();
                    }

                    cmd.CommandText = "DELETE FROM Vendors WHERE VendorID=@VendorID";
                    parameters.AddWithValue("@VendorID", vendorID);
                    record += (int)cmd.ExecuteNonQuery();

                    if (record >= 1)
                    {
                        trans.Commit();
                        this.resultMessageVendors = Messages(MessageFor.SucssesDelete);
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    this.resultMessageVendors = Messages(MessageFor.TransactionCancel) + "\n" + ex.Message;
                }

            }
            finally
            {
                CloseSqlConnection();
            }
        }

        public bool IsVendorNameExistInDb(string vendorName)
        {
            return IsValueExistsInDB("Vendors", "VendorName", vendorName);
        }

        public override string GetCRUDResult()
        {
            return resultMessageVendors;
        }
    }
}