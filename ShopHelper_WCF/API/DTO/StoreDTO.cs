using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ShopHelper_WCF.API.DTO
{
    [DataContract]
    public class StoreDTO:BaseDTO
    {
        private string storeName;
        private string storeOldName;

        [DataMember]
        public string StoreName
        {
            get { return storeName; }
            set { storeName = value; }
        }

        [DataMember]
        public string StoreOldName
        {
            get { return storeOldName; }
            set { storeOldName = value; }
        }
    }
}