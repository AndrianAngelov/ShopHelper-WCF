using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ShopHelper_WCF.API.DTO
{
    [DataContract]
    public class CommonDTO
    {
        string productOldName;
        string vendorOldName;
        string result;

        [DataMember]
        public string ProductOldName
        {
            get { return productOldName; }
            set { productOldName = value; }
        }

        [DataMember]
        public string VendorOldName
        {
            get { return vendorOldName; }
            set { vendorOldName = value; }
        }

        [DataMember]
        public string Result
        {
            get { return result; }
            set { result = value; }
        }
    }
}