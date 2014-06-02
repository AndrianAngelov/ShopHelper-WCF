using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ShopHelper_WCF.API.DTO
{
    [DataContract]
    public class CategoryDTO:BaseDTO
    {
        private string categoryName;
        private string categoryOldName;

        [DataMember]
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        [DataMember]
        public string CategoryOldName
        {
            get { return categoryOldName; }
            set { categoryOldName = value; }
        }
    }
}