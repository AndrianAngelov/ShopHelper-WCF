using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ShopHelper_WCF.API.DTO
{
    [DataContract]
    public class VendorDTO:BaseDTO
    {
        private string vendorName;
        private string vendorOldName;

        [DataMember]
        public string VendorName
        {
            get { return vendorName; }
            set { vendorName = value; }
        }

         [DataMember]
        public string VendorOldName
        {
            get { return vendorOldName; }
            set { vendorOldName = value; }
        }
    }
}