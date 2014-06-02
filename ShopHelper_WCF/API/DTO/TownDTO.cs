using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ShopHelper_WCF.API.DTO
{
    [DataContract]
    public class TownDTO:BaseDTO
    {
        private string townName;
        private string townOldName;

        [DataMember]
        public string TownName
        {
            get { return townName; }
            set { townName = value; }
        }

        [DataMember]
        public string TownOldName
        {
            get { return townOldName; }
            set { townOldName = value; }
        }
    }
}