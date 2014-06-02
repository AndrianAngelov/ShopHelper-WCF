using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ShopHelper_WCF.API.DTO;

namespace ShopHelper_WCF.API.BLL
{
    [ServiceContract]
    public interface IVendor
    //public e v ADO_WCF
    //public interface IVendor
    {
        [OperationContract]
        List<VendorDTO> GetVendors();
    }
}
