using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ShopHelper_WCF.API.DTO
{
    [DataContract]
    //[KnownType(typeof(ProductDTO))] //taka e w ADO_WCF
    public class ProductDTO:BaseDTO
    {
        //int productID;
        string productOldName;
        string productNewName;
        decimal productPrice;
        string vendorName;
        int quantity;
        string categoryName;
        string storeName;
        string townName;

        [DataMember]
        public string ProductOldName
        {
            get { return productOldName; }
            set { productOldName = value; }
        }

        [DataMember]
        public string ProductNewName
        {
            get { return productNewName; }
            set { productNewName = value; }
        }

        [DataMember]
        public decimal ProductPrice
        {
            get { return productPrice; }
            set { productPrice = value; }
        }

        [DataMember]
        public string VendorName
        {
            get { return vendorName; }
            set { vendorName = value; }
        }

        [DataMember]
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        [DataMember]
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        [DataMember]
        public string StoreName
        {
            get { return storeName; }
            set { storeName = value; }
        }

        [DataMember]
        public string TownName
        {
            get { return townName; }
            set { townName = value; }
        }
    }
}