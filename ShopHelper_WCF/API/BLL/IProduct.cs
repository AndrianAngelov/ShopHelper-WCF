using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ShopHelper_WCF.API.DTO;

namespace ShopHelper_WCF.API.BLL
{
    //[ServiceKnownType("GetKnownTypes", typeof(HelperDTO))]
    [ServiceContract]
    public interface IProduct
    //public e v ADO_WCF
    //public interface IProduct
    {
        [OperationContract]
        List<ProductDTO> GetProducts();
    }
}
